using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Drivers : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_SystemDriver");

            foreach (ManagementObject entry in searcher.Get())
            {
                if (entry.GetPropertyValue("PathName") != null)
                {
                    string path = entry.GetPropertyValue("PathName").ToString();

                    bool exists = true;

                    if (!File.Exists(path))
                    {
                        exists = false;
                    }

                    list.Add(new List<KeyValuePair<string, string>>() {
                        new KeyValuePair<string, string>("token", "Drv"),
                        new KeyValuePair<string, string>("path", path),
                        new KeyValuePair<string, string>("description", "(" + entry.GetPropertyValue("Description").ToString() + ")"),
                        new KeyValuePair<string, string>("signed", exists ? !Authenticode.IsSigned(path) ? "[b](unsigned)[/b]" : null : "[b](file not found)[/b]"),
                    });
                }
            }

            report.Add(list);

            return true;
        }
    }
}
