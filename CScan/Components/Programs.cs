using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class Programs : IComponent
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var registryKey = @"Software\Microsoft\Windows\CurrentVersion\Uninstall";

            using (var key = Registry.LocalMachine.OpenSubKey(registryKey))
            {
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using (var subKey = key.OpenSubKey(subKeyName))
                    {
                        var displayName = (string) subKey.GetValue("DisplayName");
                        var installLocation = (string) subKey.GetValue("InstallLocation");

                        if (displayName != null)
                        {
                            list.Add(
                                new Dictionary<string, string>
                                {
                                    {"token", "Prg"},
                                    {"display_name", displayName},
                                    {
                                        "install_location", installLocation != "" ? "[b]" + installLocation + "[/b]" : null
                                    }
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