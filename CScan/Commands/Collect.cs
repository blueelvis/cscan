using System.Collections.Generic;

namespace CScan.Commands
{
    internal class Collect : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            foreach (var line in arguments)
            {
                list.Add(new Dictionary<string, string>
                {
                    {"token", "Collect"},
                    {"line", line}
                });
            }

            return list;
        }
    }
}