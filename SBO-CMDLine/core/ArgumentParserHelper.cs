using System;
using System.Windows.Forms;
using Mono.Options;
using SBO_CMDLine.business.company;
using SBO_CMDLine.commands;

namespace SBO_CMDLine.core
{
    public class ArgumentParserHelper
    {
        private static OptionSet _options;
        private static string _connectionMode;
        private static Command _cmd;
        private static string _connectionFile;

        private static readonly string MODE_UI = "ui";
        private static readonly string MODE_DI = "di";

        public static void ParseArgs(string[] args)
        {
            _cmd = null;
            _connectionMode = null;
            _connectionFile = null;
            bool helpMode = false;

            _options = new OptionSet()
            {
                $"SBO Command Line Helper v{Application.ProductVersion}",
                "By M.Shams 2021",
                "",
                "Options:",
                {
                    "f=", "Connection filename for DI connection mode.\nVALUE: <FILEPATH>", f => _connectionFile = f
                },
                {
                    "m=", "Connection mode.\nVALUE: UI, DI", m => _connectionMode = m
                },
                {
                    "c", "Company information tools.", _ => _cmd = new CmdCompanyInfo()
                },
                {
                    "n", "Working with UI menus.", _ => _cmd = new CmdMenuTools()
                },
                {
                    "r", "Report management tools.", _ => _cmd = new CmdReportManager()
                },
                {
                    "h|?", "show help", _ =>
                    {
                        helpMode = true;
                        ShowHelp();
                    }
                },
            };

            var extraArguments = _options.Parse(args);

            try
            {
                if (!String.IsNullOrEmpty(_connectionMode))
                {
                    if (_connectionMode.Equals(MODE_UI, StringComparison.OrdinalIgnoreCase))
                    {
                        CompanyHelper.ConnectUI();
                    }
                    else if (_connectionMode.Equals(MODE_DI, StringComparison.OrdinalIgnoreCase))
                    {
                        CompanyHelper.ConnectDI(_connectionFile);
                    }
                    else
                    {
                        Console.WriteLine("Error! Unknown connection mode.");
                    }

                    if (CompanyHelper.GetDICompany().Connected)
                    {
                        _cmd?.Process(extraArguments?.ToArray());
                        CompanyHelper.Disconnect();
                    }
                }
                else
                {
                    if (!helpMode)
                        ShowHelp();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine();
                ShowHelp();
            }
        }

        private static void ShowHelp()
        {
            _options.WriteOptionDescriptions(Console.Out);
        }
    }
}