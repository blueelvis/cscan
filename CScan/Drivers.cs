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
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
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

                    list.Add(new Dictionary<string, string>() {
                        { "token", "Drv" },
                        { "path", path },
                        { "description", "(" + entry.GetPropertyValue("Description").ToString() + ")" },
                        { "signed", exists ? !Authenticode.IsSigned(path) ? "[b](unsigned)[/b]" : null : "[b](file not found)[/b]" },
                    });
                }
            }

            report.Add(list);

            return true;
        }
    }
}
