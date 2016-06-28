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
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
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
                                new Dictionary<string, string>() {
                                    { "token", "Prg" },
                                    { "display_name", displayName },
                                    { "install_location", installLocation != "" ? "[b]" + installLocation + "[/b]" : null },
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
