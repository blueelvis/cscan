using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Disks : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Dsk"),
                    new KeyValuePair<string, string>("name", drive.Name),
                    new KeyValuePair<string, string>("label", drive.VolumeLabel),
                });
            }

            report.Add(list);

            return true;
        }
    }
}
