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
    }

    public enum FormAction
    {
        None,
        List,
        Dump
    }
}