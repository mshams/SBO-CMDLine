using System;
using System.Collections.Generic;
using System.Reflection;
using SAPbouiCOM.Framework;

namespace SBO_CMDLine
{
    class Program
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