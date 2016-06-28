using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Programs : Component
    {
        public bool Run(ref Report report, List<List<KeyValuePair<string, string>>> list)
        {
            string registryKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";

            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryKey))
            {
                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    using (Microsoft.Win32.RegistryKey subKey = key.OpenSubKey(subKeyName))
                    {
                        string displayName = (string) subKey.GetValue("DisplayName");
                        string installLocation = (string) subKey.GetValue("InstallLocation");

                        if (displayName != null)
                        {
                            list.Add(
                                new List<KeyValuePair<string, string>>() {
                                    new KeyValuePair<string, string>("token", "Prg"),
                                    new KeyValuePair<string, string>("display_name", displayName),
                                    new KeyValuePair<string, string>("install_location", installLocation != null ? "[b]" + installLocation + "[/b]" : null),
                                }
                            );
                        }
                    }
                }
            }

            report.Add(list);

            return true;
        }
    }
}
