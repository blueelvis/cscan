using System.Collections.Generic;
using System.IO;

namespace CScan.Components
{
    internal class Hosts : IComponent
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var hosts = File.ReadAllLines(@"C:\Windows\System32\drivers\etc\hosts");

            foreach (var line in hosts)
            {
                if (line != "" && line.Substring(0, 1) != "#")
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Host"},
                        {"contents", line}
                    });
                }
            }

            if (list.Count > 0)
            {
                report.Add(list);
            }

            return true;
        }
    }
}