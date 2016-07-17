using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components.HiJackThis
{
    internal class O21 : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var hasEntries = false;

            using (
                var key =
                    Registry.LocalMachine.OpenSubKey(
                        @"Software\Microsoft\Windows\CurrentVersion\ShellServiceObjectDelayLoad"))
            {
                var valueNames = key.GetValueNames();

                foreach (var valueName in valueNames)
                {
                    var value = (string) key.GetValue(valueName);
                    var dll = DllFromClsid(value);

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "O21"},
                        {"clsid", value},
                        {"name", valueName},
                        {"dll", dll == null ? "(file not found)" : dll}
                    });

                    hasEntries = true;
                }
            }

            if (hasEntries)
            {
                report.Add(list);
            }

            return true;
        }

        public string DllFromClsid(string clsid)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Classes\CLSID\" + clsid))
            {
                if (key == null)
                {
                    return null;
                }

                return (string) key.GetValue(null);
            }
        }
    }
}