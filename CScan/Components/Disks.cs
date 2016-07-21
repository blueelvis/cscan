using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;

namespace CScan.Components
{
    internal class Disks : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_LogicalDisk");

            foreach (ManagementObject drive in searcher.Get())
            {
                var caption = drive.GetPropertyValue("Caption").ToString();

                list.Add(new Dictionary<string, string>
                {
                    {"token", "Disk"},
                    {"caption", caption},
                    {"format", "[" + drive.GetPropertyValue("FileSystem") + "]"}
                });
            }

            report.Add(list);
        }
    }
}