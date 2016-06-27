using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components.HiJackThis
{
    class O20 : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows"))
            {
                string value = (string) key.GetValue("AppInit_DLLs");

                if (value != "")
                {
                    list.Add(new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("token", "O20"),
                        new KeyValuePair<string, string>("value", value),
                    });

                    report.Add(list);
                }
            }

            return true;
        }
    }
}
