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
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
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
                        new Dictionary<string, string> {
                            { "token", "Shl" },
                            { "key", @"HKLM\..\Winlogon: [Shell]" },
                            { "value", shell },
                        }
                    );
                }

                if (userinit != null)
                {
                    list.Add(
                        new Dictionary<string, string> {
                            { "token", "Shl" },
                            { "key", @"HKLM\..\Winlogon: [Userinit]" },
                            { "value", userinit },
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
                        new Dictionary<string, string> {
                            { "token", "Shl" },
                            { "key", @"HKCU\..\Winlogon: [Shell]" },
                            { "value", shell },
                        }
                    );
                }

                if (userinit != null)
                {
                    list.Add(
                        new Dictionary<string, string> {
                            { "token", "Shl" },
                            { "key", @"HKCU\..\Winlogon: [Userinit]" },
                            { "value", userinit },
                        }
                    );
                }
            }

            report.Add(list);

            return true;
        }
    }
}
