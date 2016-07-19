using System.Collections.Generic;

namespace CScan.Commands
{
    internal class RunCommand : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            foreach (string line in arguments)
            {
                list.Add(new Dictionary<string, string>()
                {
                    {"token", "Run"},
                    {"line", line},
                });
            }

            return list;
        }
    }
}