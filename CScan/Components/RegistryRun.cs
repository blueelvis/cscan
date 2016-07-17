using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class RegistryRun : Component
    {
        protected string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

        protected string RunOnceKey = @"Software\Microsoft\Windows\CurrentVersion\RunOnce";

        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var registryKey in ConsolidateKeys())
            {
                var splitKeys = registryKey.Split('\\');

                var lastKey = splitKeys[splitKeys.Length - 1];

                using (var key = Registry.LocalMachine.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey, "HKLM");
                }

                using (var key = Registry.CurrentUser.OpenSubKey(registryKey))
                {
                    list = IterateOverValues(list, key, lastKey, "HKCU");
                }
            }

            report.Add(list);

            return true;
        }

        protected string[] ConsolidateKeys()
        {
            return new[]
            {
                RunKey,
                RunOnceKey
            };
        }

        protected List<Dictionary<string, string>> IterateOverValues(List<Dictionary<string, string>> list,
            RegistryKey key, string type, string root)
        {
            foreach (var valueName in key.GetValueNames())
            {
                if (key.GetValueKind(valueName) == RegistryValueKind.String)
                {
                    var value = (string) key.GetValue(valueName);

                    list.Add(
                        new Dictionary<string, string>
                        {
                            {"token", "Run"},
                            {"key", root + @"\..\" + type + ": [[b]" + valueName + "[/b]]"},
                            {"value", value}
                        }
                        );
                }
            }

            return list;
        }
    }
}