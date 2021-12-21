using System;
using System.Collections.Generic;
using SAPbouiCOM;
using SBO_CMDLine.business.company;

namespace SBO_CMDLine.business.ui
{
    public class MenuHelper
    {
        private static readonly string FORMAT = "UID: {0,-10}\tName: {1}";

        /// <summary>
        /// Get list of all menus
        /// </summary>
        /// <param name="verboseMode">Return verbose information</param>
        /// <param name="uid">List submenus of given UID</param>
        /// <returns></returns>
        public static List<string> GetMenuList(bool verboseMode, string uid)
        {
            List<String> list = new List<string>();
            Menus menus;

            if (string.IsNullOrEmpty(uid))
            {
                menus = CompanyHelper.GetApplication().Menus;
            }
            else
            {
                MenuItem menu = CompanyHelper.GetApplication().Menus.Item(uid);
                menus = menu.SubMenus;

                list.Add(String.Format("[{2}]UID: {0,-10}\tName: {1}",
                    menu.UID,
                    menu.String,
                    menu.SubMenus != null ? "+" : "-"));
            }

            string verboseFormat = "   [{2}]UID: {0,-10}\tName: {1}";

            if (menus != null)
                if (verboseMode)
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

            return list;
        }

        /// <summary>
        /// Find menu by pattern
        /// </summary>
        /// <param name="searchItem">id:<UID>, name:<SUBSTRING></param>
        /// <returns></returns>
        public static List<string> FindMenu(string searchItem)
        {
            List<String> list = new List<string>();

            var menus = CompanyHelper.GetApplication().Menus;

            bool modeId = searchItem.StartsWith("id:");
            string param = searchItem.Split(':')[1];

            for (int i = 0; i < menus.Count; i++)
                TraverseMenuItems(menus.Item(i), modeId, param, ref list);

            return list;
        }

        /// <summary>
        /// Recursive menu traversal
        /// </summary>
        /// <param name="item"></param>
        /// <param name="modeId"></param>
        /// <param name="searchItem"></param>
        /// <param name="list"></param>
        private static void TraverseMenuItems(MenuItem item, bool modeId, string searchItem, ref List<string> list)
        {
            if (modeId)
            {
                if (item.UID.Equals(searchItem))
                    list.Add(string.Format(FORMAT, item.UID, item.String));
            }
            else
            {
                if (item.String.Replace("&", "").ToLower().Contains(searchItem.ToLower()))
                    list.Add(string.Format(FORMAT, item.UID, item.String));
            }

            Menus menus = item.SubMenus;
            if (menus == null) return;

            for (int i = 0; i < menus.Count; i++)
                TraverseMenuItems(menus.Item(i), modeId, searchItem, ref list);
        }
    }
}