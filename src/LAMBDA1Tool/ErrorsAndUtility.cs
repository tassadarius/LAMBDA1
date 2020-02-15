using System;
using System.Collections.Generic;

namespace LAMBDA1Tool
{
    /// <summary>
    /// Handles utility stuff like printing the help and license, as well as error messages and exiting. 
    /// </summary>
    /// Important! Do not forget to instantiate the options like this:
    /// <example> <c>
    ///     var errorAndUtility = ErrorsAndUtility.Instance;
    ///     errorAndUtility.options = options;
    /// </c> </example>
    public sealed class ErrorsAndUtility
    {

        // Singleton constructors
        static ErrorsAndUtility() { }
        private ErrorsAndUtility() {}

        // Singleton get Instance function
        public static ErrorsAndUtility Instance { get; } = new ErrorsAndUtility();

        // This option dictionary must be set in order to call printHelp()
        public Dictionary<char, (string, string, bool)> options;

        // The various error strings
        public static string keySizeErrMsg = "The keysize must be {0} bytes. However {1} bytes were provided.";
        public static string missingKeyArgErrMsg = "An error occured while reading the key. Make sure -k key is specified correctly.";
        public static string unknownArgparseErrMsg = "An unknown error occured on parsing the arguments. Did you specify the arguments correctly?";
        public static string unknownModeErrMsg = "I do not know what to do (encrypt, decrypt create-key?), please look at the help page.";
        public static string keyInputErrMsg = "Error on reading the key. Opening the file returned:\n\n\"{0}\"";
        public static string keyOutputErrMsg = "Error on writing the key. Opening the file returned:\n\n\"{0}\"";
        public static string inputIOErrMsg = "Error on reading the input. Opening the file returned:\n\n\"{0}\"";
        public static string outputIOErrMsg = "Error on writing the output. Opening the file returned:\n\n\"{0}\"";
        public static string fileNotFoundErrMsg = "Could not find file '{0}'.";
        public static string invalidArgCountErrMsg = "Invalid number of positional arguments given. Expected none, 1 or 2 arguments, but {0} were provided.";
        public static string decodeErrMsg = "Error on decoding the key. The base64 decoding function returned:\n\n\"{0}\".\n" +
            "Note that this may be the cause of a misspelled filename interpreted as base64.";

        /// <summary>
        /// Prints an error message and exits with given code. Allows to print the help above the error.
        /// </summary>
        /// <param name="reason">The quit message displayed.</param>
        /// <param name="code">The exit code</param>
        /// <param name="printHelp">Specifies wether the help will be printed above the quit message</param>
        public void CleanErrorExit(string reason, int code, bool printHelp)
        {
            if (printHelp)
            {
                PrintHelp();
                Console.WriteLine();
            }

            Console.Error.WriteLine(reason);
            Environment.Exit(code);
        }

        /// <summary>
        /// Prints the help message (either by calling -h or when triggered by erros).
        /// Note that the ErrorsAndUtility.options dictionary must be set.
        /// </summary>
        public void PrintHelp()
        {
            if (CheckCorrectInstantiation())
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
        }

        /// <summary>
        /// Prints the licensing message as suggested by the FSF
        /// </summary>
        public void PrintLicense()
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

        /// <summary>
        /// Checks if the class has been correctly instantiated before printing the help. I know this is not optimal 
        /// but this is only a small program. Writes to stderr on mistake.
        /// </summary>
        /// <returns>returns true if the class has been correctly initialized and PrintHelp() is safe to call</returns>
        private bool CheckCorrectInstantiation()
        {
            if (options == null)
            {
                Console.Error.WriteLine("PROGRAMMING ERROR. You did not instantiate ErrorsAndUtility.options " +
                    "before calling PrintHelp(). I will skip the printing");
                return false;
            } else
            {
                return true;
            }
        }

    }
}
