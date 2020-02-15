using Lambda;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace LAMBDA1Tool
{
    class Program
    {

        private static readonly Dictionary<char,(string, string, bool)> options = new Dictionary<char, (string, string, bool)>()
        {
            {'h', ("help", "Print this help", false)},
            {'k', ("key", "Provide a KEY as hexadecimal input string or file", true)},
            {'e', ("encrypt", "Encrypt the input. May read from a file or if not provided stdin. Same goes for output", false)},
            {'d', ("decrypt", "Decrypt the input. May read from a file or if not provided stdin. Same goes for output", false)},
            {'c', ("create-key", "Create a key. May not be called with -e or -d", false)},
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

            List<(string,string)> keywordArgs = null;
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
                ReadKey(keywordArgs, out var keyData);
                ReadInput(input, out var inputData);
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
                ReadKey(keywordArgs, out var keyData);
                ReadInput(input, out var inputData);
                input.Close();

                var encryptionEngine = new CipherFeedbackMode(keyData);
                encryptionEngine.DecryptData(inputData, out var outputData);
                IOHandler.HandleOutputIO(Constants.ProgramMode.Decrypt, positionalArgs, out var output);
                output.Write(outputData);
                output.Flush();
                output.Close();
            }

            // Create key
            else if (createKey && !encrypt && !decrypt)
            {
                CreateKey(out var keyData);
                IOHandler.HandleOutputIO(Constants.ProgramMode.CreateKey, positionalArgs, out var output);
                var encoded_key = Convert.ToBase64String(keyData);

                // This sloppy loop is due to the BinaryWriter I use for both binary output and textual key.
                // We truncate the more significant byte with the cast to byte. Since the output of base64
                // should always be an ASCII character this works fine.
                foreach (var character in encoded_key.ToCharArray())
                    output.BaseStream.WriteByte((byte)character);
                output.Close();
            }

            // Unknown combination (e. g. encrypt and decrypt specified at same time)
            else
                errorAndUtility.CleanErrorExit(ErrorsAndUtility.unknownModeErrMsg, 1, true);
        }

        /// <summary>
        /// Reads bytes from a BinaryReader. Can handle files as well as streams, but it uses a not-so-nice
        /// try/catch for it.
        /// </summary>
        /// <param name="input">The prepared input handle</param>
        /// <param name="output">the bytes read from the input handle until EOF is reached</param>
        private static void ReadInput(BinaryReader input, out byte[] output)
        {
            List<byte> buffer = new List<byte>();

            // I know it's not clean to do this by an exception. However I don't know how else to do it,
            // as a Stream input from the terminal does not know it's end.
            try
            {
                while (true)
                    buffer.Add(input.ReadByte());
            } catch (EndOfStreamException)
            {
                // do nothing here
            }
            output = buffer.ToArray();
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
        private static void ReadKey(List<(string, string)> keywordArgs, out byte[] key)
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
                if (File.Exists(keyArg))
                {
                    var rawKey = File.ReadAllText(@keyArg);
                    rawKey = Regex.Replace(rawKey, @"\t|\n|\r|\s", "");
                    key = Base64Decode(rawKey);
                }
                else
                {
                    key = Base64Decode(keyArg);
                }

                if (key != null && key.Length != Lambda1.KeySize)
                {
                    var errorAndUtility = ErrorsAndUtility.Instance;
                    errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.keySizeErrMsg, Lambda1.KeySize, key.Length), 1, false);
                }
                    
            } catch (FormatException)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(ErrorsAndUtility.decodeErrMsg, 1, false);
            } catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.keyInputErrMsg, e.Message), 1, false);
            }
                
        }

        private static byte[] Base64Decode(string data)
        {
            return Convert.FromBase64String(data);
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
