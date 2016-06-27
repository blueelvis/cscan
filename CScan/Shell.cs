using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Shell : Component
    {
        public bool Run(ref Report report, List<List<KeyValuePair<string, string>>> list)
        {
            string registryKey = @"Software\Microsoft\Windows NT\CurrentVersion\Winlogon";

            string[] splitKeys = registryKey.Split('\\');

            string lastKey = splitKeys[splitKeys.Length - 1];

            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryKey))
            {
                string shell = (string)key.GetValue("Shell");
                string userinit = (string)key.GetValue("Userinit");

                if (shell != null)
                {
                    list.Add(
                        new List<KeyValuePair<string, string>>() {
                            new KeyValuePair<string, string>("token", "Shl"),
                            new KeyValuePair<string, string>("key", @"HKLM\..\Winlogon: [Shell]"),
                            new KeyValuePair<string, string>("value", shell),
                        }
                    );
                }

                if (userinit != null)
                {
                    list.Add(
                        new List<KeyValuePair<string, string>>() {
                            new KeyValuePair<string, string>("token", "Shl"),
                            new KeyValuePair<string, string>("key", @"HKLM\..\Winlogon: [Userinit]"),
                            new KeyValuePair<string, string>("value", userinit),
                        }
                    );
                }
            }

            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKey))
            {
                string shell = (string)key.GetValue("Shell");
                string userinit = (string)key.GetValue("Userinit");

                if (shell != null)
                {
                    list.Add(
                        new List<KeyValuePair<string, string>>() {
                            new KeyValuePair<string, string>("token", "Shl"),
                            new KeyValuePair<string, string>("key", @"HKCU\..\Winlogon: [Shell] =>"),
                            new KeyValuePair<string, string>("shell", shell),
                        }
                    );
                }

                if (userinit != null)
                {
                    list.Add(
                        new List<KeyValuePair<string, string>>() {
                            new KeyValuePair<string, string>("token", "Shl"),
                            new KeyValuePair<string, string>("key", @"HKCU\..\Winlogon: [Userinit]"),
                            new KeyValuePair<string, string>("value", userinit),
                        }
                    );
                }
            }

            report.Add(list);

            return true;
        }
    }
}
