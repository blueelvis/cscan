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
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
        {
            foreach (DriveInfo drive in DriveInfo.GetDrives())
            {
                try
                {
                    list.Add(new Dictionary<string, string>() {
                        { "token", "Dsk" },
                        { "name", drive.Name },
                        { "label", drive.VolumeLabel },
                    });
                } catch (IOException)
                {
                    //
                }
            }

            report.Add(list);

            return true;
        }
    }
}
