using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBO_CMDLine.attribute
{
    [System.AttributeUsage(System.AttributeTargets.Method, AllowMultiple = false)]
    public class SwitchAttribute : System.Attribute
    {
        public int Action;

        public SwitchAttribute(int action)
        {
            Action = action;
        }

        public SwitchAttribute(object action)
        {
            Action = (int) action;
        }
    }
}