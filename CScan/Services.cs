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
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
        {
            ServiceController[] services = ServiceController.GetServices();

            List<ServiceController> sortedServices = services.OrderBy(service => service.DisplayName).ToList();

            foreach (ServiceController service in sortedServices)
            {
                list.Add(new Dictionary<string, string>() {
                    { "token", "Svc" },
                    { "name", "[b]" + service.ServiceName + "[/b]" },
                    { "imagePath", GetImagePath(service.ServiceName) },
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
