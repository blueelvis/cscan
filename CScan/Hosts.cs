using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Hosts : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            string[] hosts = File.ReadAllLines(@"C:\Windows\System32\drivers\etc\hosts");

            foreach (string line in hosts)
            {
                if (line != "" && line.Substring(0, 1) != "#")
                {
                    list.Add(new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("token", "Hst"),
                        new KeyValuePair<string, string>("contents", line),
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
