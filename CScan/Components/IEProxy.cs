using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class IEProxy : IComponent
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var registryPath = @"Software\Microsoft\Windows\CurrentVersion\Internet Settings";

            var keyHKCU = Registry.CurrentUser;
            var key = keyHKCU.OpenSubKey(registryPath);

            var enabled = key.GetValue("ProxyEnable").ToString();

            if (enabled == "1")
            {
                list.Add(new Dictionary<string, string>
                {
                    {"token", "Prx"},
                    {"server", key.GetValue("ProxyServer").ToString()}
                });

                report.Add(list);
            }

            return true;
        }
    }
}