using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class IFEO : IComponent
    {
        private readonly string registryPath =
            @"Software\Microsoft\Windows NT\currentversion\image file execution options";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(registryPath))
            {
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using (var subKey = key.OpenSubKey(subKeyName))
                    {
                        if (subKey.GetValue("Debugger") != null)
                        {
                            list.Add(new Dictionary<string, string>
                            {
                                {"token", "IFEO"},
                                {"subkey_name", @"HKLM\..\IFEO\" + subKeyName + ": [Debugger]"},
                                {"debugger_value", (string) subKey.GetValue("Debugger")}
                            });
                        }
                    }
                }
            }

            report.Add(list);
        }

        public bool IsActive(string productStateHex)
        {
            return productStateHex.Substring(1, 1) == "1";
        }
    }
}