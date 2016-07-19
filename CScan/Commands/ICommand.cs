using System.Collections.Generic;

namespace CScan.Commands
{
    internal interface ICommand
    {
        List<List<Dictionary<string, string>>> Run(List<string> lines, List<List<Dictionary<string, string>>> list);
    }
}