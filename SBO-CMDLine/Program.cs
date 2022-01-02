using System;
using System.Collections.Generic;
using System.Reflection;
using SAPbobsCOM;
using SAPbouiCOM.Framework;
using SBO_CMDLine.business.company;
using SBO_CMDLine.core;
using Application = SAPbouiCOM.Framework.Application;

namespace SBO_CMDLine
{
    class Program : Application
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            ArgumentParserHelper.ParseArgs(args);
        }
    }
}