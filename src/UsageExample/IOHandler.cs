using System;
using System.Collections.Generic;
using System.IO;

namespace LAMBDA1Tool
{
    static class IOHandler
    {
        
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

        public static void HandleOutputIO(Constants.ProgramMode mode, List<string> positionalArgs, out BinaryWriter output)
        {
            output = null;
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
