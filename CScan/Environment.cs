using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Environment : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
        {
            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("token", "Env"),
                new KeyValuePair<string, string>("name", "%TEMP%:"),
                new KeyValuePair<string, string>("value", System.Environment.GetEnvironmentVariable("TEMP")),
            });

            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("token", "Env"),
                new KeyValuePair<string, string>("name", "%PATH%:"),
                new KeyValuePair<string, string>("value", System.Environment.GetEnvironmentVariable("PATH")),
            });

            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("token", "Env"),
                new KeyValuePair<string, string>("name", "%USERPROFILE%:"),
                new KeyValuePair<string, string>("value", System.Environment.GetEnvironmentVariable("USERPROFILE")),
            });

            list.Add(new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("token", "Env"),
                new KeyValuePair<string, string>("name", "%SYSTEMROOT%:"),
                new KeyValuePair<string, string>("value", System.Environment.GetEnvironmentVariable("SYSTEMROOT")),
            });

            report.Add(list);

            return true;
        }
    }
}
