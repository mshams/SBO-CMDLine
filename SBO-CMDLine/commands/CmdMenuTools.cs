using System;
using System.Collections.Generic;
using Mono.Options;
using SBO_CMDLine.attribute;
using SBO_CMDLine.business.ui;
using Command = SBO_CMDLine.cmd.Command;


namespace SBO_CMDLine.commands
{
    public class CmdMenuTools : Command
    {
        public string SearchItem;
        public string ListItem;

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
                        "list:", "Get list of menus.\nVALUE: <UID>", f =>
                        {
                            ListItem = f;
                            Action = MenuAction.List;
                        }
                    },
                    {
                        "find=", "Find menu by name or id.\nVALUE: id:<UID>, name:<SUBSTRING>", f =>
                        {
                            SearchItem = f;
                            Action = MenuAction.Find;
                        }
                    }
                };
            }
        }

        [Switch(MenuAction.Find)]
        public void SwitchFind()
        {
            List<string> list = MenuHelper.FindMenu(SearchItem);
            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }

        [Switch(MenuAction.List)]
        public void SwitchList()
        {
            List<string> list = MenuHelper.GetMenuList(VerboseMode, ListItem);
            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }
    }

    public enum MenuAction
    {
        None,
        List,
        Find
    }
}