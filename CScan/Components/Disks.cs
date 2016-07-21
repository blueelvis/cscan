using System.Collections.Generic;
using System.IO;

namespace CScan.Components
{
    internal class Disks : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var drive in DriveInfo.GetDrives())
            {
                try
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Disk"},
                        {"name", drive.Name},
                        {"label", drive.VolumeLabel},
                        {"format", "[" + drive.DriveFormat + "]"}
                    });
                }
                catch (IOException)
                {
                    //
                }
            }

            report.Add(list);
        }

        private string BytesToGB(double bytes)
        {
            return bytes/1000000000 + " GB";
        }
    }
}