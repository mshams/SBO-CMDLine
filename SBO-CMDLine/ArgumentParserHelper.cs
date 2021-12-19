using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.VisualStyles;
using Mono.Options;
using SBO_CMDLine.commands;

namespace SBO_CMDLine
{
    public class ArgumentParserHelper
    {
        private static OptionSet options;

        public static void ParseArgs(string[] args)
        {
            bool result = true;
            Command cmd = null;

            options = new OptionSet()
            {
                "SBO Command Line Helper",
                "If no message is specified, a generic greeting is used.",
                "",
                "Options:",
                {
                    "r|report", "Install/Uninstall report.", _ => cmd = new CmdReportManager()
                },
                {
                    "h|?|help", "show help", _ => ShowHelp()
                },
            };


            var extraArguments = options.Parse(args);
            cmd?.Process(extraArguments?.ToArray());
        }

        private static void ShowHelp()
        {
            options.WriteOptionDescriptions(Console.Out);
        }
    }
}