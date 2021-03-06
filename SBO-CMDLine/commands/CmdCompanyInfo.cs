using System;
using Mono.Options;
using SBO_CMDLine.attribute;
using SBO_CMDLine.business.company;
using Command = SBO_CMDLine.core.Command;

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
                        "list", "Get list of companies.", _ => Action = CompanyAction.List
                    }
                };
            }
        }

        [Switch(CompanyAction.List)]
        public void SwitchList()
        {
            var list = CompanyHelper.GetCompanyList(VerboseMode);
            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }
    }

    public enum CompanyAction
    {
        None,
        List
    }
}