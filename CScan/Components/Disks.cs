using System.Collections.Generic;
using System.IO;

namespace CScan.Components
{
    internal class Disks : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Disk"},
                        {"name", drive.Name},
                        {"label", drive.VolumeLabel}
                    });
                }
                catch (IOException)
                {
                    //
                }
            }

            report.Add(list);

            return true;
        }
    }
}