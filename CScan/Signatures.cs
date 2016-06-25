using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Signatures : Component
    {
        public string[] files =
        {
            @"C:\Windows\System32\explorer.exe",
        };

        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            foreach (string file in files)
            {
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Sig"),
                    new KeyValuePair<string, string>("file", file),
                    new KeyValuePair<string, string>("signed", !Authenticode.IsSigned(file, true) ? "[b]is not signed[/b]" : "is signed"),
                });
            }

            report.Add(list);

            return true;
        }
    }
}
