using System.Collections.Generic;
using Microsoft.Win32;
using CScan.RegistryWrapper;

namespace CScan.Components
{
    internal class RegistryRun : IComponent
    {
        protected string RunKey = @"Software\Microsoft\Windows\CurrentVersion\Run";

        protected string RunOnceKey = @"Software\Microsoft\Windows\CurrentVersion\RunOnce";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var registryKey in ConsolidateKeys())
            {
                var splitKeys = registryKey.Split('\\');

                var lastKey = splitKeys[splitKeys.Length - 1];

                foreach (RegistryResult result in RegistryWrapper.RegistryWrapper.QuerySubKey(RegistryHive.LocalMachine, registryKey))
                {
                    list = IterateOverValues(list, result.key, lastKey, "HKLM", result.view == RegistryView.Registry64);
                }

                foreach (RegistryResult result in RegistryWrapper.RegistryWrapper.QuerySubKey(RegistryHive.CurrentUser, registryKey))
                {
                    list = IterateOverValues(list, result.key, lastKey, "HKCU", result.view == RegistryView.Registry64);
                }
            }

            report.Add(list);
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
            RegistryKey key, string type, string root, bool sixtyFour)
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
                            {"key", root + @"\..\" + type + ": " + (sixtyFour ? "(x64)" : "") + " [[b]" + valueName + "[/b]]"},
                            {"value", value}
                        }
                    );
                }
            }

            return list;
        }
    }
}