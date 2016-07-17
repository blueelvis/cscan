using System.Collections.Generic;
using System.IO;
using System.Management;

namespace CScan.Components
{
    internal class Drivers : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SystemDriver");

            foreach (ManagementObject entry in searcher.Get())
            {
                if (entry.GetPropertyValue("PathName") != null)
                {
                    var path = entry.GetPropertyValue("PathName").ToString();

                    var exists = File.Exists(path);

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Drv"},
                        {"path", path},
                        {"description", "(" + entry.GetPropertyValue("Description") + ")"},
                        {"exists", !exists ? "[b](file not found)[/b]" : null}
                    });
                }
            }

            report.Add(list);

            return true;
        }
    }
}