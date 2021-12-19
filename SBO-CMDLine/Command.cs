using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;

namespace SBO_CMDLine
{
    public abstract partial class Command
    {
        protected bool VerboseMode = false;

        public abstract string Description { get; }
        public abstract string Help { get; }
        public abstract OptionSet Options { get; }

        public void ShowHelp()
        {
            this.Options?.WriteOptionDescriptions(Console.Out);
        }

        public void Process(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    ShowHelp();
                }
                else
                    this.Options?.Parse(args);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error! {e.Message}\n");
                ShowHelp();
            }
        }
    }
}