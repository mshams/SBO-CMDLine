using System;
using System.IO;
using Mono.Options;
using SBO_CMDLine.business.report;

namespace SBO_CMDLine.commands
{
    public class CmdReportManager : Command
    {
        public string Filename;
        public string ReportName;
        public string TypeName;
        public string Typecode;
        public string FormType;
        public string AddonName;
        public string FindString;
        public string Menu;
        public InstallAction Action;

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
                        "install", "Install report.", _ => Action = InstallAction.Install
                    },
                    {
                        "uninstall", "Uninstall report.", _ => Action = InstallAction.Uninstall
                    },
                    {
                        "list", "List installed reports.", _ => Action = InstallAction.List
                    },
                    {
                        "find=", "Find report by type or name.\nVALUE: type:<TYPECODE>, name:<SUBSTRING>", searchItem =>
                        {
                            FindString = searchItem;
                            Action = InstallAction.Find;
                        }
                    }
                };
            }
        }

        public override void PreProcess()
        {
            base.PreProcess();

            Action = InstallAction.None;
            Filename = "";
            ReportName = "";
            Typecode = "";
            TypeName = "";
            FormType = "";
            AddonName = "";
            Menu = "";
        }

        public override void PostProcess()
        {
            base.PostProcess();

            if (Action == InstallAction.Install)
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
            else if (Action == InstallAction.Uninstall)
            {
                if (string.IsNullOrEmpty(Typecode))
                    throw new Exception("Need report type code to uninstall it.");

                ReportHelper.RemoveReport(
                    Typecode,
                    TypeName,
                    FormType,
                    AddonName);
            }
            else if (Action == InstallAction.List)
            {
                var list = ReportHelper.GetReportList();

                string str = string.Join("\n", list.ToArray());
                Console.WriteLine(str);
            }
            else if (Action == InstallAction.Find)
            {
                bool modeType = FindString.StartsWith("type:");
                string param = FindString.Split(':')[1];

                var list = ReportHelper.GetReportList(
                    modeType ? param : "",
                    modeType ? "" : param);

                string str = string.Join("\n", list.ToArray());
                Console.WriteLine(str);
            }
        }
    }

    public enum InstallAction
    {
        None,
        Install,
        Uninstall,
        List,
        Find
    }
}