using System;
using System.Collections.Generic;
using System.IO;
using Mono.Options;
using SBO_CMDLine.attribute;
using SBO_CMDLine.business.report;
using SBO_CMDLine.business.ui;
using Command = SBO_CMDLine.cmd.Command;

namespace SBO_CMDLine.commands
{
    public class CmdReportManager : cmd.Command
    {
        public string Filename;
        public string ReportName;
        public string TypeName;
        public string Typecode;
        public string FormType;
        public string AddonName;
        public string FindString;
        public string Menu;

        public override string Description => "Install/Uninstall reports.";

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
                        "file:", "Report file to do action on.\nVALUE: <FILEPATH>", f => Filename = f
                    },
                    {
                        "report:", "Report name to install.\nVALUE: <NAME>", f => ReportName = f
                    },
                    {
                        "type:", "Type name to install.\nVALUE: <NAME>", f => TypeName = f
                    },
                    {
                        "code:", "Type code to install.\nVALUE: <NAME>", f => Typecode = f
                    },
                    {
                        "form:", "Form name to install.\nVALUE: <NAME>", f => FormType = f
                    },
                    {
                        "addon:", "Addon name to install.\nVALUE: <NAME>", f => AddonName = f
                    },
                    {
                        "menu:", "Menu ID to install report.\nVALUE: <NAME>", f => Menu = f
                    },
                    {
                        "install", "Install report.", _ => Action = ReportAction.Install
                    },
                    {
                        "uninstall", "Uninstall report.", _ => Action = ReportAction.Uninstall
                    },
                    {
                        "list", "List installed reports.", _ => Action = ReportAction.List
                    },
                    {
                        "find=", "Find report by type or name.\nVALUE: type:<TYPECODE>, name:<SUBSTRING>", searchItem =>
                        {
                            FindString = searchItem;
                            Action = ReportAction.Find;
                        }
                    }
                };
            }
        }

        [Switch(ReportAction.Find)]
        public void SwitchFind()
        {
            bool modeType = FindString.StartsWith("type:");
            string param = FindString.Split(':')[1];

            var list = ReportHelper.GetReportList(
                modeType ? param : "",
                modeType ? "" : param);

            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }

        [Switch(ReportAction.List)]
        public void SwitchList()
        {
            var list = ReportHelper.GetReportList();

            string str = string.Join("\n", list.ToArray());
            Console.WriteLine(str);
        }

        [Switch(ReportAction.Install)]
        public void SwitchInstall()
        {
            if (string.IsNullOrEmpty(Filename))
                throw new Exception("Need file to install as report.");

            if (!File.Exists(Filename))
                throw new Exception("Report file doesn't exists.");

            ReportHelper.ImportReport(
                Filename,
                ReportName,
                TypeName,
                FormType,
                AddonName,
                Menu);
        }

        [Switch(ReportAction.Uninstall)]
        public void SwitchUninstall()
        {
            if (string.IsNullOrEmpty(Typecode))
                throw new Exception("Need report type code to uninstall it.");

            ReportHelper.RemoveReport(
                Typecode,
                TypeName,
                FormType,
                AddonName);
        }
    }

    public enum ReportAction
    {
        None,
        Install,
        Uninstall,
        List,
        Find
    }
}