using System.Collections.Generic;
using System.ServiceProcess;
using Microsoft.Win32;
using CScan.Whitelist;

namespace CScan.Components
{
    internal class Services : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var services = ServiceController.GetServices();

            foreach (var service in services)
            {
                var imagePath = GetImagePath(service.ServiceName).Trim();
                var name = service.ServiceName.Trim();

                if (ServiceWhitelist.IsWhitelisted(imagePath, name))
                    continue;

                list.Add(new Dictionary<string, string>
                {
                    {"token", "Svc"},
                    {"name", "[b]" + name + "[/b]"},
                    {"imagePath", imagePath}
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