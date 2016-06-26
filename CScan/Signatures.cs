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
            @"C:\Windows\explorer.exe",
            @"C:\Windows\System32\userinit.exe",
            @"C:\Windows\System32\svchost.exe",
            @"C:\Windows\System32\wininit.exe",
            @"C:\Windows\System32\Drivers\volsnap.sys",
            @"C:\Windows\System32\User32.dll",
            System.Reflection.Assembly.GetExecutingAssembly().Location,
        };

        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            foreach (string file in files)
            {
                bool exists = File.Exists(file);

                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Sig"),
                    new KeyValuePair<string, string>("file", file),
                    new KeyValuePair<string, string>("signed", exists ? !Authenticode.IsSigned(file, true) ? "[b]is not signed[/b]" : "is signed" : "[b]does not exist[/b]"),
                });
            }

            report.Add(list);

            return true;
        }
    }
}
