using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class Services : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var services = ServiceController.GetServices();

            var sortedServices = services.OrderBy(service => service.DisplayName).ToList();

            foreach (var service in sortedServices)
            {
                list.Add(new Dictionary<string, string>
                {
                    {"token", "Svc"},
                    {"name", "[b]" + service.ServiceName + "[/b]"},
                    {"imagePath", GetImagePath(service.ServiceName)}
                });
            }

            report.Add(list);

            return true;
        }

        private string GetImagePath(string serviceName)
        {
            var registryPath = @"SYSTEM\CurrentControlSet\Services\" + serviceName;

            var keyHKLM = Registry.LocalMachine;
            var key = keyHKLM.OpenSubKey(registryPath);

            var value = key.GetValue("ImagePath").ToString();

            key.Close();

            return value;
        }
    }
}