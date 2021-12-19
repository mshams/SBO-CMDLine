using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;
using SAPbouiCOM;
using SBO_CMDLine.business.company;

namespace SBO_CMDLine.commands
{
    public class CmdMenuTools : Command
    {
        private const string FORMAT = "UID: {0,-10}\tName: {1}";

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
                            List<String> list = new List<string>();
                            Menus menus;

                            if (string.IsNullOrEmpty(u))
                            {
                                menus = CompanyHelper.GetApplication().Menus;
                            }
                            else
                            {
                                MenuItem menu = CompanyHelper.GetApplication().Menus.Item(u);
                                menus = menu.SubMenus;

                                list.Add(String.Format("[{2}]UID: {0,-10}\tName: {1}",
                                    menu.UID,
                                    menu.String,
                                    menu.SubMenus != null ? "+" : "-"));
                            }

                            string verboseFormat = "   [{2}]UID: {0,-10}\tName: {1}";

                            if (menus != null)
                                if (VerboseMode)
                                {
                                    for (int i = 0; i < menus.Count; i++)
                                    {
                                        MenuItem menu = menus.Item(i);
                                        list.Add(String.Format(verboseFormat,
                                            menu.UID,
                                            menu.String,
                                            menu.SubMenus != null ? "+" : "-"
                                        ));
                                    }
                                }
                                else
                                {
                                    for (int i = 0; i < menus.Count; i++)
                                    {
                                        MenuItem menu = menus.Item(i);
                                        list.Add(menu.String);
                                    }
                                }

                            string str = string.Join("\n", list.ToArray());
                            Console.WriteLine(str);
                        }
                    },
                    {
                        "find=", "Find menu by name or id.\nVALUE: id:<UID>, name:<SUBSTRING>", searchItem =>
                        {
                            List<String> list = new List<string>();

                            var menus = CompanyHelper.GetApplication().Menus;

                            bool modeId = searchItem.StartsWith("id:");
                            string param = searchItem.Split(':')[1];

                            for (int i = 0; i < menus.Count; i++)
                                TraverseMenuItems(menus.Item(i), modeId, param, ref list);

                            string str = string.Join("\n", list.ToArray());
                            Console.WriteLine(str);
                        }
                    }
                };
            }
        }

        private void TraverseMenuItems(MenuItem item, bool modeId, string searchItem, ref List<string> list)
        {
            if (modeId)
            {
                if (item.UID.Equals(searchItem))
                    list.Add(string.Format(FORMAT, item.UID, item.String));
            }
            else
            {
                if (item.String.Contains(searchItem))
                    list.Add(string.Format(FORMAT, item.UID, item.String));
            }

            Menus menus = item.SubMenus;
            if (menus == null) return;

            for (int i = 0; i < menus.Count; i++)
                TraverseMenuItems(menus.Item(i), modeId, searchItem, ref list);
        }
    }
}