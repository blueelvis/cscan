using System.Collections.Generic;

namespace CScan.Components
{
    internal class Environment : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            list.Add(new Dictionary<string, string>
            {
                {"token", "Env"},
                {"name", "%TEMP%:"},
                {"value", System.Environment.GetEnvironmentVariable("TEMP")}
            });

            list.Add(new Dictionary<string, string>
            {
                {"token", "Env"},
                {"name", "%PATH%:"},
                {"value", System.Environment.GetEnvironmentVariable("PATH")}
            });

            list.Add(new Dictionary<string, string>
            {
                {"token", "Env"},
                {"name", "%USERPROFILE%:"},
                {"value", System.Environment.GetEnvironmentVariable("USERPROFILE")}
            });

            list.Add(new Dictionary<string, string>
            {
                {"token", "Env"},
                {"name", "%SYSTEMROOT%:"},
                {"value", System.Environment.GetEnvironmentVariable("SYSTEMROOT")}
            });

            report.Add(list);

            return true;
        }
    }
}