using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;
using SBO_CMDLine.business.company;

namespace SBO_CMDLine.commands
{
    public class CmdCompanyInfo : Command
    {
        public override string Description => "Get company information.";
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
                        "verbose", "Verbose information.", _ => VerboseMode = true
                    },
                    {
                        "list", "Get list of companies.", _ =>
                        {
                            var list = CompanyHelper.GetCompanyList(VerboseMode);
                            string str = string.Join("\n", list.ToArray());
                            Console.WriteLine(str);
                        }
                    }
                };
            }
        }
    }
}