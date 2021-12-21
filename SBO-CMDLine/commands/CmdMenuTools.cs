using System;
using System.Collections.Generic;
using Mono.Options;
using SBO_CMDLine.business.ui;

namespace SBO_CMDLine.commands
{
    public class CmdMenuTools : Command
    {
        public override string Description => "Working with UI menus.";
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
                        "list:", "Get list of menus.\nVALUE: <UID>", u =>
                        {
                            List<string> list = MenuHelper.GetMenuList(VerboseMode, u);
                            string str = string.Join("\n", list.ToArray());
                            Console.WriteLine(str);
                        }
                    },
                    {
                        "find=", "Find menu by name or id.\nVALUE: id:<UID>, name:<SUBSTRING>", searchItem =>
                        {
                            List<string> list = MenuHelper.FindMenu(searchItem);
                            string str = string.Join("\n", list.ToArray());
                            Console.WriteLine(str);
                        }
                    }
                };
            }
        }
    }
}