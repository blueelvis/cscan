using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Header : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("raw", Main.name + " Version " + Main.version),
            });

            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("raw", "Running as " + System.Security.Principal.WindowsIdentity.GetCurrent().Name + " on Windows " + Environment.OSVersion.Version),
            });


            dynamic totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;

            totalMemory = (int)(totalMemory / 1000000000);

            dynamic freeMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;

            freeMemory = (int)(freeMemory / 1000000000);

            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("raw", totalMemory + "GB RAM installed; " + freeMemory + "GB RAM available"),
            });

            report.Add(list);

            return true;
        }
    }
}
