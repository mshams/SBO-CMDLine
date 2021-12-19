using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;

namespace SBO_CMDLine.commands
{
    public class CmdReportManager : Command
    {
        public string Filename;

        public override string Description => "Install/Uninstall reports.";

        public override string Help => "";

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
                        "install=", "Name/Path of the file to be installed.", f =>
                        {
                            //Filename = f;
                            //Console.WriteLine($"Filename: {Filename}");
                        }
                    },
                    {
                        "uninstall=", "Name/Path of the file to be uninstalled.", f => Filename = f
                    }
                };
            }
        }
    }
}