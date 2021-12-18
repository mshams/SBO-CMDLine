using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SBO_CMDLine
{
    interface IArgumentCommand
    {
        bool Process(String[] args);
        string GetName();
        string GetDescription();
    }
}