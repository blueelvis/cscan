using System.Collections.Generic;
using Microsoft.Win32;
using System.Management;
using System;

namespace CScan.Components
{
    internal class WMI : IComponent
    {
        private string wmiPath = @"\\" + System.Environment.MachineName + @"\root\SecurityCenter2";

        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var searcher = new ManagementObjectSearcher(wmiPath, "SELECT * FROM AntivirusProduct");
            var collection = searcher.Get();

            if (collection.Count == 0)
                return true;

            foreach (ManagementObject item in collection)
            {
                var productState = item.GetPropertyValue("productState").ToString().ToString("X");
                var displayName = (string)item.GetPropertyValue("displayName");

                Console.WriteLine(item.GetPropertyValue("productState"));
            }

            return true;
        }
    }
}