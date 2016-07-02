using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Integrity : Component
    {
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
        {
            string registryKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

            using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(registryKey))
            {
                if (key != null && key.GetValue("DisableTaskMgr") != null && (int)key.GetValue("DisableTaskMgr") != 0)
                {
                    AddToList(list, "HKCU", "System", "DisableTaskMgr", key.GetValue("DisableTaskMgr").ToString());
                }
            }

            if (list.Count > 0)
            {
                report.Add(list);
            }

            return true;
        }

        private void AddToList(List<Dictionary<string, string>> list, string hive, string subKey, string valueName, string value)
        {
            list.Add(new Dictionary<string, string>
            {
                {"token", "Int"},
                {"key", hive + @"\..\" + subKey + ": [" + valueName + "]"},
                {"value", value},
            });
        }
    }
}
