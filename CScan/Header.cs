using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Header : Component
    {
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
        {
            list.Add(new Dictionary<string, string>()
            {
                {"raw", Main.name + " Version " + Main.version},
            });

            CultureInfo ci = CultureInfo.InstalledUICulture;

            list.Add(new Dictionary<string, string>()
            {
                {"raw", "Running from " + System.Reflection.Assembly.GetExecutingAssembly().Location.Substring(0, 3) + " as " + System.Security.Principal.WindowsIdentity.GetCurrent().Name},
            });

            list.Add(new Dictionary<string, string>()
            {
                {"raw", "Windows Version " + System.Environment.OSVersion.Version + " Language " + ci.EnglishName},
            });

            Double totalMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory;
            totalMemory = totalMemory / 1000000000;

            Double freeMemory = new Microsoft.VisualBasic.Devices.ComputerInfo().AvailablePhysicalMemory;
            freeMemory = freeMemory / 1000000000;

            list.Add(new Dictionary<string, string>()
            {
                {"raw", totalMemory.ToString("N1") + "GB RAM installed; " + freeMemory.ToString("N1") + "GB RAM available"},
            });

            report.Add(list);

            return true;
        }
    }
}
