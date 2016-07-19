using System.Collections.Generic;
using Microsoft.Win32;
using System.Management;
using System;

namespace CScan.Components
{
    internal class SecurityCenter : IComponent
    {
        private readonly string wmiPath = @"\\" + System.Environment.MachineName + @"\root\SecurityCenter2";

        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var searcher = new ManagementObjectSearcher(wmiPath, "SELECT * FROM AntivirusProduct");
            var collection = searcher.Get();

            if (collection.Count == 0)
                return true;

            foreach (ManagementObject item in collection)
            {
                var productState = int.Parse(item.GetPropertyValue("productState").ToString());
                var productStateHex = productState.ToString("X");

                var displayName = (string) item.GetPropertyValue("displayName");

                list.Add(new Dictionary<string, string>()
                {
                    { "token", "SecurityCenter" },
                    { "display_name", displayName },
                    { "is_active", IsActive(productStateHex) ? "(Active)" : "(Inactive)" },
                    { "product_state", "[" + productStateHex + "]" },
                });

                Console.WriteLine(productStateHex);
            }

            report.Add(list);

            return true;
        }

        public bool IsActive(string productStateHex)
        {
            return productStateHex.Substring(1, 1) == "1";
        }
    }
}