using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SBO_CMDLine.commands;

namespace SBO_CMDLine
{
    public class ArgumentParserHelper
    {
        public static bool ParseArgs(string[] args)
        {
            bool showHelp = false;
            bool result = false;

            IArgumentCommand cmd;

            var p = new Mono.Options.OptionSet()
            {
                {
                    "ir|report", "Report Manager", _ =>
                    {
                        cmd = new CmdReportManager();
                        cmd.Process(args);
                    }
                },
                {"h|help", "show help", _ => showHelp = true},
            };


            if (showHelp || args.Length == 0)
            {
                p.WriteOptionDescriptions(Console.Out);
                return false;
            }

            return result;
        }
    }
}