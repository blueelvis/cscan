using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class RegistryRun : Component
    {
        protected string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

        protected string RunOnceKey = @"Software\Microsoft\Windows\CurrentVersion\RunOnce";

        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (string registryKey in ConsolidateKeys())
            {
                string[] splitKeys = registryKey.Split('\\');

                string lastKey = splitKeys[splitKeys.Length - 1];

                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey, "HKLM");
                }

                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey, "HKCU");
                }
            }

            report.Add(list);

            return true;
        }

        protected string[] ConsolidateKeys()
        {
            return new string[] {
                RunKey,
                RunOnceKey,
            };
        }

        protected List<Dictionary<string, string>> IterateOverValues(List<Dictionary<string, string>> list, Microsoft.Win32.RegistryKey key, string type, string root)
        {
            foreach (string valueName in key.GetValueNames())
            {
                if (key.GetValueKind(valueName) == Microsoft.Win32.RegistryValueKind.String)
                {
                    string value = (string)key.GetValue(valueName);

                    list.Add(
                        new Dictionary<string, string>() {
                            { "token", "Run" },
                            { "key", root + @"\..\" + type + ": [[b]" + valueName + "[/b]]" },
                            { "value", value },
                        }
                    );
                }
            }

            return list;
        }
    }
}
