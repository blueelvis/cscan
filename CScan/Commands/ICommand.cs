using System.Collections.Generic;

namespace CScan.Commands
{
    internal interface ICommand
    {
        List<Dictionary<string, string>> Run(List<string> lines, List<Dictionary<string, string>> list);
    }
}