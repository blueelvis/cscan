using System.Collections.Generic;

namespace CScan.Commands
{
    internal interface Command
    {
        List<Dictionary<string, string>> Run(List<Dictionary<string, string>> list, string[] arguments);
    }
}