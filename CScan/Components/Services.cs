using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class Services : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var services = ServiceController.GetServices();

            foreach (var service in services)
            {
                list.Add(new Dictionary<string, string>
                {
                    {"token", "Svc"},
                    {"name", "[b]" + service.ServiceName + "[/b]"},
                    {"imagePath", GetImagePath(service.ServiceName)}
                });
            }

            list.Sort((entry1, entry2) => entry1["name"].CompareTo(entry2["name"]));

            report.Add(list);
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