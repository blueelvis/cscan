using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.Commands
{
    internal class RunCommand : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            foreach (var line in arguments)
            {
                var hive = line.Substring(0, 4) == "HKCU" ? RegistryHive.CurrentUser : RegistryHive.LocalMachine;
                var value = line.Substring(5);

                foreach (
                    var result in
                        RegistryWrapper.RegistryWrapper.QuerySubKey(hive,
                            @"Software\Microsoft\Windows\CurrentVersion\Run"))
                {
                    result.key.DeleteValue(value, false);

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Run"},
                        {"key", "Deleted " + value}
                    });
                }
            }

            return list;
        }
    }
}