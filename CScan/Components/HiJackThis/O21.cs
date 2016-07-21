using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components.HiJackThis
{
    internal class O21 : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
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
                    var dll = Clsid.GetFile(value);

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "O21"},
                        {"clsid", value},
                        {"name", valueName},
                        {"dll", string.IsNullOrEmpty(dll) ? "(file not found)" : dll}
                    });

                    

                    hasEntries = true;
                }
            }

            if (hasEntries)
            {
                report.Add(list);
            }
        }
    }
}