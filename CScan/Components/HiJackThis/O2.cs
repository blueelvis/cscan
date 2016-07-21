using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components.HiJackThis
{
    internal class O2 : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Browser Helper Objects"))
            {
                if (key == null)
                    return;

                foreach (string subKeyName in key.GetSubKeyNames())
                {
                    using (var subKey = key.OpenSubKey(subKeyName))
                    {
                        string fileName = Clsid.GetFile(subKeyName);
                        string friendly = Clsid.GetName(subKeyName);

                        list.Add(new Dictionary<string, string>
                        {
                            {"token", "O2"},
                            {"name", friendly},
                            {"clsid", subKeyName},
                            {"file", fileName}
                        });
                    }
                }
            }
        }
    }
}