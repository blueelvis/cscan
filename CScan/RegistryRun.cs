using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class RegistryRun : Component
    {
        protected string[] RunKeys =
        {
            "Software\\Microsoft\\Windows\\CurrentVersion\\Run",
        };

        protected string[] RunOnceKeys =
        {
            "Software\\Microsoft\\Windows\\CurrentVersion\\RunOnce",
        };

        public bool Run(ref Report report, List<List<KeyValuePair<string, string>>> list)
        {
            foreach (string registryKey in ConsolidateKeys())
            {
                string[] splitKeys = registryKey.Split('\\');

                string lastKey = splitKeys[splitKeys.Length - 1];

                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey);
                }

                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey);
                }
            }

            report.Add(list);

            return true;
        }

        protected string[] ConsolidateKeys()
        {
            return RunKeys.Concat(RunOnceKeys).ToArray();
        }

        protected List<List<KeyValuePair<string, string>>> IterateOverValues(List<List<KeyValuePair<string, string>>> list, Microsoft.Win32.RegistryKey key, string type)
        {
            foreach (string valueName in key.GetValueNames())
            {
                if (key.GetValueKind(valueName) == Microsoft.Win32.RegistryValueKind.String)
                {
                    string value = (string)key.GetValue(valueName);

                    list.Add(
                        new List<KeyValuePair<string, string>> () {
                            new KeyValuePair<string, string>("token", type),
                            new KeyValuePair<string, string>("key", valueName + " =>"),
                            new KeyValuePair<string, string>("value", "[b]" + value + "[/b]"),
                        }
                    );
                }
            }

            return list;
        }
    }
}
