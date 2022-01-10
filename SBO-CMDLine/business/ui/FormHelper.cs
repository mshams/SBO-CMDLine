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
    }
}