using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Services : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            ServiceController[] services = ServiceController.GetServices();

            List<ServiceController> sortedServices = services.OrderBy(service => service.DisplayName).ToList();

            foreach (ServiceController service in sortedServices)
            {
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Svc"),
                    new KeyValuePair<string, string>("name", "[b]" + service.ServiceName + "[/b]"),
                    new KeyValuePair<string, string>("imagePath", GetImagePath(service.ServiceName)),
                });
            }

            report.Add(list);

            return true;
        }

        private string GetImagePath(string serviceName)
        {
            string registryPath = @"SYSTEM\CurrentControlSet\Services\" + serviceName;

            RegistryKey keyHKLM = Registry.LocalMachine;
            RegistryKey key = keyHKLM.OpenSubKey(registryPath);

            string value = key.GetValue("ImagePath").ToString();

            key.Close();

            return value;
        }
    }
}
