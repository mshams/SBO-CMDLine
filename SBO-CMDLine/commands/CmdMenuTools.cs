using System;
using System.Collections.Generic;
using Mono.Options;
using SBO_CMDLine.business.ui;

namespace SBO_CMDLine.commands
{
    public class CmdMenuTools : Command
    {
        public string SearchItem;
        public string ListItem;
        public MenuAction Action;

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

        public override void PostProcess()
        {
            base.PostProcess();

            if (Action == MenuAction.List)
            {
                List<string> list = MenuHelper.GetMenuList(VerboseMode, ListItem);
                string str = string.Join("\n", list.ToArray());
                Console.WriteLine(str);
            }
            else if (Action == MenuAction.Find)
            {
                List<string> list = MenuHelper.FindMenu(SearchItem);
                string str = string.Join("\n", list.ToArray());
                Console.WriteLine(str);
            }
        }

        public override void PreProcess()
        {
            base.PreProcess();

            Action = MenuAction.None;
            SearchItem = "";
            ListItem = "";
        }
    }

    public enum MenuAction
    {
        None,
        List,
        Find
    }
}