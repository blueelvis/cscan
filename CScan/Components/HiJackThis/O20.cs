using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components.HiJackThis
{
    internal class O20 : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows NT\CurrentVersion\Windows"))
            {
                var value = (string) key.GetValue("AppInit_DLLs");

                if (value != "")
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "O20"},
                        {"value", value}
                    });

                    report.Add(list);
                }
            }
        }
    }
}