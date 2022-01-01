using System;
using System.Linq;
using System.Reflection;
using Mono.Options;
using SBO_CMDLine.attribute;

namespace SBO_CMDLine.cmd
{
    /// <summary>
    /// Base class to be used as template for commands
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// To determine selected action in command.
        /// </summary>
        public Enum Action { get; set; }

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

        /// <summary>
        /// Invoke the selected action.
        /// </summary>
        public virtual void PostProcess()
        {
            if (Action != null)
            {
                var selectedAction = Convert.ToInt32(Action);
                var switchMethods = this.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly);

                foreach (var method in switchMethods)
                {
                    var attributes = method.GetCustomAttributes(typeof(SwitchAttribute), true);
                    if (attributes.Any())
                    {
                        int methodAction = ((SwitchAttribute) attributes.First()).Action;
                        if (selectedAction == methodAction)
                        {
                            method.Invoke(this, null);
                        }
                    }
                }
            }
        }

        public virtual void PreProcess()
        {
        }
    }
}