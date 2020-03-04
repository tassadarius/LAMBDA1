using Lambda;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace LAMBDA1Tool
{
    class Program
    {

        private static readonly Dictionary<char, (string, string, bool)> options = new Dictionary<char, (string, string, bool)>()
        {
            {'h', ("help", "Print this help", false)},
            {'k', ("key", "Provide a KEY as base64 encoded string or a file; required for encrypting/decrypting.", true)},
            {'e', ("encrypt", "Encrypt given message (stdin or INPUT). Output either goes to stdout or OUTPUT.", false)},
            {'d', ("decrypt", "Decrypt given ciphertext (stdin or INPUT). Output either goes to stdout or OUTPUT", false)},
            {'c', ("create-key", "Create a key and writes to stdout or file if provided OUTPUT. May not be called with --encrypt or -decrypt.", false)},
            {'l', ("license", "Print license information", false)},
        };

        static void Main(string[] args)
        {

            // Instantiate error handling and utility singleton.
            var errorAndUtility = ErrorsAndUtility.Instance;
            //  Give it the arguments so it knows what to print in printHelp()
            errorAndUtility.options = options;

            /*
             * Argument parsing using my badly written custom argument parser
             */
            bool help = false, key = false, encrypt = false, decrypt = false, createKey = false, license = false;

            List<(string, string)> keywordArgs = null;
            List<string> positionalArgs = null;
            var parser = new ArgumentParser(options);
            try
            {
                parser.Parse(args, out positionalArgs, out keywordArgs);
            }
            catch (ArgumentException e)
            {
                errorAndUtility.CleanErrorExit(e.Message, 1, true);
            }

            /*
             * Set program modes according to specified args
             */
            foreach (var argument in keywordArgs)
            {
                switch (argument.Item1)
                {
                    case "h":
                        help = true;
                        break;
                    case "k":
                        key = true;
                        break;
                    case "e":
                        encrypt = true;
                        break;
                    case "d":
                        decrypt = true;
                        break;
                    case "c":
                        createKey = true;
                        break;
                    case "l":
                        license = true;
                        break;
                    default:
                        errorAndUtility.CleanErrorExit(ErrorsAndUtility.unknownArgparseErrMsg, 1, true);
                        break;
                }
            }

            /*
             * Execute according to the specified args. 
             */
            if (help)
            {
                errorAndUtility.PrintHelp();
                Environment.Exit(0);
            }

            else if (license)
            {
                errorAndUtility.PrintLicense();
                Environment.Exit(0);
            }

            // Encrypt
            else if (encrypt && !decrypt && key && !createKey)
            {
                IOHandler.HandleInputIO(positionalArgs, out var input);
                GetKey(keywordArgs, out var keyData);
                IOHandler.ReadInput(input, out var inputData);
                input.Close();

                var encryptionEngine = new CipherFeedbackMode(keyData);
                encryptionEngine.EncryptData(inputData, out var outputData);
                IOHandler.HandleOutputIO(Constants.ProgramMode.Encrypt, positionalArgs, out var output);
                output.Write(outputData);
                output.Flush();
                output.Close();
            }

            // Decrypt
            else if (!encrypt && decrypt && key && !createKey)
            {
                IOHandler.HandleInputIO(positionalArgs, out var input);
                GetKey(keywordArgs, out var keyData);
                IOHandler.ReadInput(input, out var inputData);
                input.Close();

                var encryptionEngine = new CipherFeedbackMode(keyData);
                encryptionEngine.DecryptData(inputData, out var outputData);
                IOHandler.HandleOutputIO(Constants.ProgramMode.Decrypt, positionalArgs, out var output);
                output.Write(outputData);
                output.Flush();
                output.Close();
            }

            // Create key
            else if (createKey && !encrypt && !decrypt && !key)
            {
                CreateKey(out var keyData);
                IOHandler.HandleOutputIO(Constants.ProgramMode.CreateKey, positionalArgs, out var output);
                var encodedKey = Convert.ToBase64String(keyData);
                IOHandler.writeEncodedKey(output, encodedKey);
                output.Close();
            }

            // Encrypt or Decrypt specified correctly, but no key specified
            else if ((encrypt && !decrypt) || (decrypt && !encrypt) && !key)
            {
                var mode = encrypt ? "encrypt" : "decrypt";
                errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.noKeySpecifiedErrMsg, mode), 1, true);
            }

            // Create key (--create-key) and load key (--key) specified at same time
            else if (createKey && key)
                errorAndUtility.CleanErrorExit(ErrorsAndUtility.keyCreateAndLoadErrMsg, 1, true);

            // Unknown combination (e. g. encrypt and decrypt specified at same time)
            else
                errorAndUtility.CleanErrorExit(ErrorsAndUtility.unknownModeErrMsg, 1, true);
        }


        /// <summary>
        /// Processes the key input whiuch is expected to be either a base64 string or a file.
        /// Overview of method: Finds argument --> checks if its a file --> reads from file or directly uses argument.
        /// The key is expected to base64. That means if the input was recognized as file, it is read and the content
        /// base64 decoded. If it's not a file, it is directly base64 decoded.
        /// 
        /// This function checks the key for validity.
        /// </summary>
        /// <param name="keywordArgs">List of arguments where -k KEY is specified (not checked here)</param>
        /// <param name="key">Returns a valid key as bytes</param>
        private static void GetKey(List<(string, string)> keywordArgs, out byte[] key)
        {
            /*
             * In the first part find the -k argument and extract what was specified
             */
            string keyArg = null;
            key = null;
            foreach (var arg in keywordArgs)
            {
                if (arg.Item1.Equals("k"))
                    keyArg = arg.Item2;
            }
            if (keyArg == null)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(ErrorsAndUtility.missingKeyArgErrMsg, 1, true);
            }

            /*
             * In the second part, check wether the argument specified matches a file or not. If it's a file it reads
             * and decodes, if it's not a file it directly tries to decode it.
             */
            try
            {
                if (IOHandler.HandlePossibleKeyFile(keyArg, out var base64Key))
                {
                    key = Convert.FromBase64String(base64Key);
                }
                else
                {
                    key = Convert.FromBase64String(keyArg);
                }

                if (key != null && key.Length != Lambda1.KeySize)
                {
                    var errorAndUtility = ErrorsAndUtility.Instance;
                    errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.keySizeErrMsg, Lambda1.KeySize, key.Length), 1, false);
                }

            }
            catch (FormatException e)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.decodeErrMsg, e.Message), 1, false);
            }

        }


        /// <summary>
        /// Creates a valid LAMBDA1 key with 32 bytes - uses RNGCryptoServiceProvider
        /// </summary>
        /// <param name="key">the output key as bytes</param>
        private static void CreateKey(out byte[] key)
        {
            key = new byte[Lambda1.KeySize];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(key);
        }
    }
}
