using System;
using System.Collections.Generic;
using System.IO;

namespace LAMBDA1Tool
{
    /// <summary>
    /// Provides Input and Output functions for reading/writing key and binary files or stdin/stdout.
    /// </summary>
    /// The order of IO operations is as following:
    ///    0 positional arguments --> read from stdin, write to stdout
    ///    1 positional argument  --> read from arg,   write to stdout
    ///    2 positional arguments --> read from arg,   write to arg
    static class IOHandler
    {

        /// <summary>
        /// Handles input BinaryReaders from a file or stdin. The function automatically determines how many parameters
        /// have been passed and opens files accordingly.
        /// </summary>
        /// <param name="positionalArgs">The positional Args as List</param>
        /// <param name="input"> The input file handler, from which can be read</param>
        public static void HandleInputIO(List<string> positionalArgs, out BinaryReader input)
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
                        var errorAndUtility = ErrorsAndUtility.Instance;
                        errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.invalidArgCountErrMsg, positionalArgs.Count), 1, true);
                        break;
                }
            }
            catch (FileNotFoundException)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.fileNotFoundErrMsg, positionalArgs[0]), 1, false);
            }
            catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
            {
                var errorAndUtility = ErrorsAndUtility.Instance;
                errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.inputIOErrMsg, e.Message), 1, false);
            }
        }

        /// <summary>
        /// Handles output BinaryWriters to a file or stdout. The function automatically determines how many parameters
        /// have been passed and opens files accordingly.
        /// </summary>
        /// <param name="mode"> Specify wether the output is for a Key creation or Encryption/Decryption </param>
        /// <param name="positionalArgs">The positional Args as List</param>
        /// <param name="output">The output file handler, to which can be written</param>
        public static void HandleOutputIO(Constants.ProgramMode mode, List<string> positionalArgs, out BinaryWriter output)
        {
            output = null;
            /*
             * If we create a key we only have an output (no input). It can either be a file or stdout.
             */
            if (mode == Constants.ProgramMode.CreateKey)
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
                            // If someone specifies just more parameters we throw an error and quit.
                            var errorAndUtility = ErrorsAndUtility.Instance;
                            errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.invalidArgCountErrMsg, positionalArgs.Count), 1, true);
                            break;
                    }
                }
                catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                {
                    var errorAndUtility = ErrorsAndUtility.Instance;
                    errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.keyOutputErrMsg, e.Message), 1, false);
                }

            }
            /*
             * If we encrypt/decrypt we can have 3 modes. Output can either be file or stdout. See class description.
             */
            else if (mode == Constants.ProgramMode.Encrypt || mode == Constants.ProgramMode.Decrypt)
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
                            // If someone specifies just more parameters we throw an error and quit.
                            var errorAndUtility = ErrorsAndUtility.Instance;
                            errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.invalidArgCountErrMsg, positionalArgs.Count), 1, true);
                            break;
                    }
                }
                catch (Exception e) when (e is IOException || e is UnauthorizedAccessException)
                {
                    var errorAndUtility = ErrorsAndUtility.Instance;
                    errorAndUtility.CleanErrorExit(string.Format(ErrorsAndUtility.outputIOErrMsg, e.Message), 1, false);
                }

            }
        }
    }
}
