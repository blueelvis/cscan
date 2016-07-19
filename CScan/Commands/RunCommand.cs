using System.Collections.Generic;

namespace CScan.Commands
{
    internal class RunCommand : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            return list;
        }
    }
}