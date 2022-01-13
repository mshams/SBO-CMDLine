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

            string verboseFormat = "   UID: {0,-10} Type: {1,-18} Text: {2}";

            if (form != null)
                if (verboseMode)
                {
                    for (int i = 0; i < form.Items.Count; i++)
                    {
                        Item item = form.Items.Item(i);
                        string text = GetItemText(item);

                        list.Add(String.Format(verboseFormat,
                            item.UniqueID,
                            item.Type,
                            text
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
        /// Return a string of caption, text or other info for every item.
        /// </summary>
        /// <param name="item">Given item</param>
        /// <returns></returns>
        private static string GetItemText(Item item)
        {
            string result;

            try
            {
                switch (item.Type)
                {
                    case BoFormItemTypes.it_BUTTON:
                        result = ((Button) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_STATIC:
                        result = ((StaticText) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_BUTTON_COMBO:
                        result = ((ButtonCombo) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_CHECK_BOX:
                        result = ((CheckBox) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_EXTEDIT:
                    case BoFormItemTypes.it_EDIT:
                        result = ((EditText) item.Specific).String?.Replace("\n", " ")
                            .Substring(0, Math.Min(30, ((EditText) item.Specific).String.Length));

                        if (!string.IsNullOrEmpty(result))
                            result += "...";
                        break;

                    case BoFormItemTypes.it_FOLDER:
                        result = ((Folder) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_OPTION_BUTTON:
                        result = ((OptionBtn) item.Specific).Caption;
                        break;

                    case BoFormItemTypes.it_COMBO_BOX:
                        result = ((ComboBox) item.Specific).Selected?.Value;
                        break;

                    case BoFormItemTypes.it_PICTURE:
                        result = ((PictureBox) item.Specific).Picture;
                        break;

                    case BoFormItemTypes.it_WEB_BROWSER:
                        result = ((WebBrowser) item.Specific).Url;
                        break;

                    case BoFormItemTypes.it_LINKED_BUTTON:
                        var lb = (LinkedButton) item.Specific;
                        result = $"{lb.LinkedObject}:{lb.LinkedObjectType}";
                        break;

                    case BoFormItemTypes.it_GRID:
                        result = $"{((Grid) item.Specific).Rows.Count}R x {((Grid) item.Specific).Columns.Count}C";
                        break;

                    case BoFormItemTypes.it_MATRIX:
                        result = $"{((Matrix) item.Specific).RowCount}R x {((Matrix) item.Specific).Columns.Count}C";
                        break;

                    case BoFormItemTypes.it_PANE_COMBO_BOX:
                        result = ((PaneComboBox) item.Specific).Selected?.Value;
                        break;

                    case BoFormItemTypes.it_ACTIVE_X:
                        result = ((ActiveX) item.Specific).ClassID;
                        break;

                    default:
                        result = "";
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return result;
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

        /// <summary>
        /// Get top form information as string.
        /// </summary>
        /// <param name="verboseMode"></param>
        /// <returns>Return verbose information</returns>
        public static string GetTopForm(bool verboseMode)
        {
            string result = "";
            Form form = CompanyHelper.GetApplication().Forms.ActiveForm;

            string verboseFormat = "   UID: {0,-10} Type: {1,-10} Mode: {2,-10}\tTitle: {3}";

            if (form != null)
                if (verboseMode)
                {
                    result = String.Format(verboseFormat,
                        form.UniqueID,
                        form.TypeEx,
                        form.Mode,
                        form.Title
                    );
                }
                else
                {
                    result = form.Title;
                }

            return result;
        }
    }
}