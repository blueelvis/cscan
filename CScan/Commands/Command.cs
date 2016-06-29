using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Commands
{
    interface Command
    {
        string command { get; }

        List<Dictionary<string, string>> Run(List<Dictionary<string, string>> list, string[] arguments);
    }
}
