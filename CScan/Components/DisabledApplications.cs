using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Components
{
    internal class DisabledApplications : Component
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var registryKey = @"Software\Microsoft\Windows\CurrentVersion\Policies\System";

            using (var key = Registry.CurrentUser.OpenSubKey(registryKey))
            {
                if (key != null && key.GetValue("DisableTaskMgr") != null && (int) key.GetValue("DisableTaskMgr") != 0)
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

        private void AddToList(List<Dictionary<string, string>> list, string hive, string subKey, string valueName,
            string value)
        {
            list.Add(new Dictionary<string, string>
            {
                {"token", "Integrity"},
                {"key", hive + @"\..\" + subKey + ": [" + valueName + "]"},
                {"value", value}
            });
        }
    }
}