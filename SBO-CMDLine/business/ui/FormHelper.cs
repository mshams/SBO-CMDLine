using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SAPbouiCOM;
using SBO_CMDLine.business.company;

namespace SBO_CMDLine.business.ui
{
    public class FormHelper
    {
        /// <summary>
        /// Get list of active forms.
        /// </summary>
        /// <param name="verboseMode">Return verbose information</param>
        /// <returns></returns>
        public static List<string> GetFormList(bool verboseMode)
        {
            List<String> list = new List<string>();
            Forms forms;
            forms = CompanyHelper.GetApplication().Forms;

            string verboseFormat = "   UID: {0,-10} Type: {1,-10} Mode: {2,-10}\tTitle: {3}";

            if (forms != null)
                if (verboseMode)
                {
                    for (int i = 0; i < forms.Count; i++)
                    {
                        Form form = forms.Item(i);
                        list.Add(String.Format(verboseFormat,
                            form.UniqueID,
                            form.TypeEx,
                            form.Mode,
                            form.Title
                        ));
                    }
                }
                else
                {
                    for (int i = 0; i < forms.Count; i++)
                    {
                        Form form = forms.Item(i);
                        list.Add(form.Title);
                    }
                }

            return list;
        }

        /// <summary>
        /// Get list of items on given menu.
        /// </summary>
        /// <param name="verboseMode">Return verbose information</param>
        /// <param name="uid">Form UID</param>
        /// <returns></returns>
        public static List<string> GetFormItems(bool verboseMode, string uid)
        {
            List<String> list = new List<string>();

            Form form = CompanyHelper.GetApplication().Forms.Item(uid);

            string verboseFormat = "   UID: {0,-10} Type: {1,-20} Description: {2}";

            if (form != null)
                if (verboseMode)
                {
                    for (int i = 0; i < form.Items.Count; i++)
                    {
                        Item item = form.Items.Item(i);
                        list.Add(String.Format(verboseFormat,
                            item.UniqueID,
                            item.Type,
                            item.Description
                        ));
                    }
                }
                else
                {
                    for (int i = 0; i < form.Items.Count; i++)
                    {
                        Item item = form.Items.Item(i);
                        list.Add(item.Type.ToString());
                    }
                }

            return list;
        }

        /// <summary>
        /// Activate given form and click on given item.
        /// </summary>
        /// <param name="formUid">Form Uid</param>
        /// <param name="itemUid">Item Uid</param>
        public static void Activate(string formUid, string itemUid)
        {
            try
            {
                Form form = Activate(formUid);
                var item = form?.Items.Item(itemUid);
                item?.Click();
            }
            catch
            {
                Console.WriteLine("Error! Form or item doesn't exists.");
            }
        }

        /// <summary>
        /// Activate given form.
        /// </summary>
        /// <param name="formUid">Form Uid</param>
        /// <returns></returns>
        public static Form Activate(string formUid)
        {
            try
            {
                Form form = CompanyHelper.GetApplication().Forms.Item(formUid);
                form?.Select();

                return form;
            }
            catch
            {
                Console.WriteLine("Error! Form doesn't exists.");
                return null;
            }
        }
    }
}