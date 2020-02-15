using System;
using System.Collections.Generic;

namespace UsageExample
{
    /// <summary>
    /// A quick and dirty hack for argument parsing. Supports positional and keyword arguments.
    /// Use with method Parse(args, out positionalArgs, out keywordArgs)
    /// </summary>
    /// Does support formats with -h or --help as well as concat as -ek.
    /// Surely has bugs in it, don't use it.
    class ArgumentParser
    {
        private readonly Dictionary<char, (string, string, bool)> options;
        private enum ArgType { Positional, Keyword }

        /// <summary>
        /// Announce what arguments are possible and initialize.
        /// </summary>
        /// 
        /// The data structure looks as follows - a dict:
        ///     key   --> letter e.g. 'h'  as character without -
        ///     value --> a 3 element Tuple (long name, description, requires argument)
        ///     
        /// Examples:  {'h', ("help", "Print help", false)}   -- only h with nothing
        ///            {'k', ("key", "Provide a key", true)}  -- k needs something which is the next parsed arg
        /// <param name="options"> The option data structure as described above </param>
        public ArgumentParser(Dictionary<char, (string, string, bool)> options)
        {
            this.options = options;
        }

        private List<char> ParseShortKeyword(string positionalArgument)
        {
            bool valid;
            List<char> result = new List<char>();
            for (int i = 1; i < positionalArgument.Length; i++)
            {
                valid = false;
                foreach (var entry in options)
                {
                    if (positionalArgument[i].Equals(entry.Key))
                    {
                        result.Add(entry.Key);
                        valid = true;
                    }
                        
                }
                if (!valid)
                    throw new ArgumentException("Argument -" + positionalArgument[i] + " not known");
            }
            return result;
        }


        private List<(string, ArgType)> ParseArgument(string argument)
        {
            List<(string, ArgType)> result = new List<(string, ArgType)>();
            // We start by checking if its a "long" keyword argument 
            if (argument.StartsWith("--"))
            {
                argument = argument.Remove(0, 2);
                foreach (var entry in options)
                {
                    if (argument.Equals(entry.Value.Item1))
                    {
                        result.Add((entry.Key.ToString(), ArgType.Keyword));
                        return result;
                    }

                }
            }
            // Else we check if it's a short keyword arg
            else if (argument.StartsWith("-"))
            {
                List<char> positionals = ParseShortKeyword(argument);
                foreach (var entry in positionals)
                    result.Add((entry.ToString(), ArgType.Keyword));
            }
            // Elseweise it's positional
            else
            {
                result.Add((argument, ArgType.Positional));
            }

            return result;
        }

        /// <summary>
        /// Parse arguments from command line
        /// </summary>
        /// <param name="args"> the args from Main </param>
        /// <param name="positionalArgs"> a List of the positional arguments </param>
        /// <param name="keywordArgs"> a Tuple of keyword arguments (type, parameter) </param>
        public void Parse(string[] args, out List<string> positionalArgs, out List<(string, string)> keywordArgs)
        {
            positionalArgs = new List<string>();
            keywordArgs = new List<(string, string)>();

            bool skipNext = false;
            List<(string, ArgType)> result;
            foreach (var argument in args)
            {

                // enter this if a keyword argument requires a parameter
                // for example -k path 
                if (skipNext)
                {
                    // append the parameter to the previous entry
                    var preceding = keywordArgs[keywordArgs.Count - 1];
                    keywordArgs[keywordArgs.Count - 1] = (preceding.Item1, argument);
                    skipNext = false;
                }
                // This is the normal path taken when parsing the next argument
                else
                {
                    result = ParseArgument(argument);
                    // We may get back multiple results, iterate over them
                    foreach (var element in result)
                    {
                        // If its a keyword add it and check if it has an argument
                        if (element.Item2 == ArgType.Keyword)
                        {
                            if (options[element.Item1.ToCharArray()[0]].Item3)
                                skipNext = true;
                            keywordArgs.Add((element.Item1, null));
                        }
                        // If its positional
                        else
                        {
                            positionalArgs.Add((element.Item1));
                        }
                    }
                }

            }
        }
        
    }
}
