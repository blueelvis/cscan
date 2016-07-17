using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using System.Security.Principal;
using Microsoft.VisualBasic.Devices;

namespace CScan.Components
{
    internal class Header : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            list.Add(new Dictionary<string, string>
            {
                {"raw", Main.name + " Version " + Main.version}
            });

            var ci = CultureInfo.InstalledUICulture;

            list.Add(new Dictionary<string, string>
            {
                {
                    "raw",
                    "Running from " + Assembly.GetExecutingAssembly().Location.Substring(0, 3) + " as " +
                    WindowsIdentity.GetCurrent().Name + " on " + DateTime.Now
                }
            });

            list.Add(new Dictionary<string, string>
            {
                {"raw", "Windows Version " + System.Environment.OSVersion.Version + " Language " + ci.EnglishName}
            });

            double totalMemory = new ComputerInfo().TotalPhysicalMemory;
            totalMemory = totalMemory/1000000000;

            double freeMemory = new ComputerInfo().AvailablePhysicalMemory;
            freeMemory = freeMemory/1000000000;

            list.Add(new Dictionary<string, string>
            {
                {
                    "raw",
                    totalMemory.ToString("N1") + "GB RAM installed; " + freeMemory.ToString("N1") + "GB RAM available"
                }
            });

            report.Add(list);

            return true;
        }
    }
}