using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;

namespace SBO_CMDLine.commands
{
    public class CmdReportManager : Command
    {
        private string filename;

        public override string Description => "Install/Uninstall reports";

        public override string Help => "Usage: Set options.";

        public override OptionSet Options
        {
            get
            {
                return new OptionSet()
                {
                    Description,
                    Help,
                    "Options:",
                    {
                        "i=|install=", "Name/Path of the file to be installed.", f =>
                        {
                            filename = f;
                            Console.WriteLine($"Filename: {filename}");
                        }
                    },
                    {
                        "u=|uninstall=", "Name/Path of the file to be uninstalled.", f => filename = f
                    }
                };
            }
        }
    }
}