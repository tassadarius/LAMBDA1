using Lambda;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace UsageExample
{
    class Program
    {
        static string keySizeExit = "The keysize must be {} bytes. However {} bytes were provided.";
        static string keyArgExit = "An error occured while reading the key. Make sure -k key is specified correctly";
        static string unknownArgparseExit = "An unknown error occured on parsing the arguments. Did you specify the arguments correctly?";
        static string unknownArgsExit = "I do not know what to do (encrypt, decrypt create-key?), please look at the help page.";

        private enum ProgramMode  { Encrypt, Decrypt, CreateKey };
        private static readonly Dictionary<char,(string, string, bool)> options = new Dictionary<char, (string, string, bool)>()
        {
            {'h', ("help", "Print this help", false)},
            {'k', ("key", "Provide a KEY as hexadecimal input string or file", true)},
            {'e', ("encrypt", "Encrypt the input. May read from a file or if not provided stdin. Same goes for output", false)},
            {'d', ("decrypt", "Decrypt the input. May read from a file or if not provided stdin. Same goes for output", false)},
            {'c', ("create-key", "Create a key. May not be called with -e or -d", false)},
            {'l', ("license", "Print license information", false)},
        };

        private static void PrintHelp()
        {
            var output = "LAMBDA1 - Encrypts/decrypts data with LAMBDA1 or creates keys";
            output += "\n\nSynopsis:    UsageExample [options] [-k KEY] INPUT OUTPUT" +
                      "\n             UsageExample -c OUTPUT";
            output += "\n\nDescription:\nLAMBDA1 is a modified version of DES, developed in Eastern Germany in\n" +
                      "the late 1980s. This program can encrypt/decrypt data as well as create keys.\n" +
                      "This is an academic implementation which is really slow.";
            output += "\n\nArguments";
            output += "\n    -h  --help        " + options['h'].Item2;
            output += "\n    -k  --key KEY     " + options['k'].Item2;
            output += "\n    -e  --encrypt     " + options['e'].Item2;
            output += "\n    -d  --decrypt     " + options['d'].Item2;
            output += "\n    -c  --create-key  " + options['c'].Item2;
            output += "\n    -l  --license     " + options['l'].Item2;
            Console.WriteLine(output);
        }

        private static void PrintLicense()
        {
            var license = "LAMBDA1 - Encrypts/decrypts data with LAMBDA1 or creates keys\n" +
                "Copyright(C) 2019  Michael Altenhuber <michael@altenhuber.net>\n\n" +

                "    This program is free software: you can redistribute it and / or modify\n" +
                "    it under the terms of the GNU General Public License as published by\n" +
                "    the Free Software Foundation, either version 3 of the License, or\n" +
                "    (at your option) any later version.\n\n" +

                "    This program is distributed in the hope that it will be useful,\n" +
                "    but WITHOUT ANY WARRANTY; without even the implied warranty of\n" +
                "    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the\n" +
                "    GNU General Public License for more details.\n\n" +

                "    You should have received a copy of the GNU General Public License\n" +
                "    along with this program. If not, see < https://www.gnu.org/licenses/>.\n";
            Console.WriteLine(license);
        }

        static void Main(string[] args)
        {

            // It seems there is no out-of-the-box command line argument parsing
            // in C#. I will workaround a little bit.
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
                CleanErrorExit(e.Message, 1, true);
            }

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
                        CleanErrorExit(unknownArgparseExit, 1, true);
                        break;
                }
            }

            if (help)
            {
                PrintHelp();
                Environment.Exit(0);
            }

            else if (license)
            {
                PrintLicense();
                Environment.Exit(0);
            }

            // Encrypt
            else if (encrypt && !decrypt && key && !createKey)
            {
                HandleInputIO(positionalArgs, out var input);
                ReadKey(keywordArgs, out var keyData);
                ReadInput(input, out var inputData);
                input.Close();

                var encryptionEngine = new CipherFeedbackMode(keyData);
                encryptionEngine.EncryptData(inputData, out var outputData);
                HandleOutputIO(ProgramMode.Encrypt, positionalArgs, out var output);
                output.Write(outputData);
                output.Flush();
                output.Close();
            }

            // Decrypt
            else if (!encrypt && decrypt && key && !createKey)
            {
                HandleInputIO(positionalArgs, out var input);
                ReadKey(keywordArgs, out var keyData);
                ReadInput(input, out var inputData);
                input.Close();

                var encryptionEngine = new CipherFeedbackMode(keyData);
                encryptionEngine.DecryptData(inputData, out var outputData);
                HandleOutputIO(ProgramMode.Decrypt, positionalArgs, out var output);
                output.Write(outputData);
                output.Flush();
                output.Close();
            }

            // Create key
            else if (createKey && !encrypt && !decrypt)
            {
                CreateKey(out var keyData);
                HandleOutputIO(ProgramMode.CreateKey, positionalArgs, out var output);
                var encoded_key = Convert.ToBase64String(keyData);

                // This sloppy loop is due to the BinaryWriter I use for both binary output and textual key.
                // We truncate the more significant byte with the cast to byte. Since the output of base64
                // should always be an ASCII character this works fine.
                foreach (var character in encoded_key.ToCharArray())
                    output.BaseStream.WriteByte((byte)character);
                output.Close();
            }

            // Unknown combination
            else
                CleanErrorExit(unknownArgsExit, 1, true);
        }

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

        private static void ReadKey(List<(string, string)> keywordArgs, out byte[] key)
        {
            string keyArg = null;
            key = null;
            foreach (var arg in keywordArgs)
            {
                if (arg.Item1.Equals("k"))
                    keyArg = arg.Item2; 
            }
            if (keyArg == null)
                CleanErrorExit(keyArgExit, 1, true);

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
                    CleanErrorExit(string.Format(keySizeExit, Lambda1.KeySize, key.Length), 1, false);
            } catch (FormatException e)
            {
                var msg = string.Format("Error on decoding the key. The base64 decoding function returned:\n\n\"{0}\"", e.Message);
                CleanErrorExit(msg, 1, false);
            } catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                CleanErrorExit(string.Format("Error on reading the key. Opening the file returned:\n\n\"{0}\"", e.Message), 1, false);
            }
                
        }

        private static void CleanErrorExit(string reason, int code, bool printHelp)
        {
            if (printHelp)
            {
                PrintHelp();
                Console.WriteLine();
            }
                
            Console.Error.WriteLine(reason);
            Environment.Exit(code);
        }

        private static byte[] Base64Decode(string data)
        {
            return Convert.FromBase64String(data);
        }

        private static void CreateKey(out byte[] key)
        {
            key = new byte[Lambda1.KeySize];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(key);
        }

        private static void HandleInputIO(List<string> positionalArgs, out BinaryReader input)
        {
            input = null;
            try
            {
                switch (positionalArgs.Count)
                {
                    case 2:
                    case 1:
                        input = new BinaryReader(new FileStream(positionalArgs[0], FileMode.Open, FileAccess.Read));
                        break;
                    case 0:
                        input = new BinaryReader(Console.OpenStandardInput());
                        break;
                    default:
                        CleanErrorExit(string.Format("Invalid number of positional arguments given." +
                            " Expected none, 1 or 2 arguments, but {0} were provided", positionalArgs.Count), 1, false);
                        break;
                }
            } catch (FileNotFoundException e)
            {
                CleanErrorExit(string.Format("Could not find file {0}", positionalArgs[0]), 1, false);
            } catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                CleanErrorExit(string.Format("Error on reading the input. Opening the file returned:\n\n\"{0}\"", e.Message), 1, false);
            }
        }

        private static void HandleOutputIO(ProgramMode mode, List<string> positionalArgs, out BinaryWriter output)
        {
            output = null;
            if (mode == ProgramMode.CreateKey)
            {
                try
                {
                    switch (positionalArgs.Count)
                    {
                        case 1:
                            output = new BinaryWriter(new FileStream(positionalArgs[0], FileMode.Create, FileAccess.Write));
                            break;
                        case 0:
                            output = new BinaryWriter(Console.OpenStandardOutput());
                            break;
                        default:
                            CleanErrorExit(string.Format("Invalid number of positional arguments given." +
                            " Expected none or 1 argument, but {0} were provided.", positionalArgs.Count), 1, false);
                            break;
                    }
                } catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                {
                    CleanErrorExit(string.Format("Error on writing the key. Opening the file returned:\n\n\"{0}\"", e.Message), 1, false);
                }
                
            }
            else if (mode == ProgramMode.Encrypt || mode == ProgramMode.Decrypt)
            {
                try
                {
                    switch (positionalArgs.Count)
                    {
                        case 2:
                            output = new BinaryWriter(new FileStream(positionalArgs[1], FileMode.Create));
                            break;
                        case 1:
                            output = new BinaryWriter(Console.OpenStandardOutput());
                            break;
                        case 0:
                            output = new BinaryWriter(Console.OpenStandardOutput());
                            break;
                        default:
                            CleanErrorExit(string.Format("Invalid number of positional arguments given." +
                            " Expected none, 1 or 2 arguments, but {0} were provided.", positionalArgs.Count), 1, false);
                            break;
                    }
                } catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                {
                    CleanErrorExit(string.Format("Error on writing the output. Opening the file returned:\n\n\"{0}\"", e.Message), 1, false);
                }

            }
        }
    }
}
