using System;
using System.Collections.Generic;
using Mono.Options;
using SBO_CMDLine.attribute;
using SBO_CMDLine.business.ui;
using Command = SBO_CMDLine.core.Command;

namespace SBO_CMDLine.commands
{
    public class CmdFormTools : Command
    {
        public string SelectedForm;
        public string ActivateItem;

        public override string Description => "Working with UI forms.";
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
                        "list", "Get list of active forms.", _ => { Action = FormAction.List; }
                    },
                    {
                        "dump=", "Get list of items on given form.\nVALUE: <UID>", f =>
                        {
                            SelectedForm = f;
                            Action = FormAction.Dump;
                        }
                    },
                    {
                        "activate=", "Active form or items.\nVALUE: <UID>[:<ITEM_ID>]", f =>
                        {
                            ActivateItem = f;
                            Action = FormAction.Activate;
                        }
                    },
                };
            }
        }

        [Switch(FormAction.List)]
        public void SwitchList()
        {
            List<string> list = FormHelper.GetFormList(VerboseMode);
            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }

        [Switch(FormAction.Dump)]
        public void SwitchDump()
        {
            List<string> list = FormHelper.GetFormItems(VerboseMode, SelectedForm);
            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }

        [Switch(FormAction.Activate)]
        public void SwitchActivate()
        {
            string[] items = ActivateItem.Split(':');
            if (items.Length > 1)
            {
                FormHelper.Activate(items[0], items[1]);
            }
            else
            {
                FormHelper.Activate(items[0]);
            }
        }
    }

    public enum FormAction
    {
        None,
        List,
        Dump,
        Activate
    }
}