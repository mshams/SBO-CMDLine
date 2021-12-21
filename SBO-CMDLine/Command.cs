using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Options;

namespace SBO_CMDLine
{
    /// <summary>
    /// Base class to be used as template for commands
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// Indicate the the command should print verbose information.
        /// </summary>
        protected bool VerboseMode = false;

        /// <summary>
        /// Description about command purpose.
        /// </summary>
        public abstract string Description { get; }

        /// <summary>
        /// Help and usage of the command.
        /// </summary>
        public abstract string Help { get; }

        /// <summary>
        /// OptionSet and switches defined ny the command. Don't define single character commands.
        /// </summary>
        public abstract OptionSet Options { get; }

        /// <summary>
        /// Print command usage and information.
        /// </summary>
        public void ShowHelp()
        {
            this.Options?.WriteOptionDescriptions(Console.Out);
        }

        /// <summary>
        /// Process input arguments by the command.
        /// </summary>
        /// <param name="args"></param>
        public void Process(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {
                    ShowHelp();
                }
                else
                {
                    PreProcess();
                    this.Options?.Parse(args);
                    PostProcess();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error! {e.Message}\n");
                ShowHelp();
            }
        }

        public virtual void PostProcess()
        {
        }

        public virtual void PreProcess()
        {
        }
    }
}