using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CScan.Components
{
    internal class Processes : IComponent
    {
        public bool Run(ref Report report, List<Dictionary<string, string>> list)
        {
            var processes = Process.GetProcesses();

            var sortedProcesses = processes.OrderBy(process => process.Id).ToList();

            foreach (var process in sortedProcesses)
            {
                if (process.Id == 0)
                {
                    continue;
                }

                string path;

                try
                {
                    path = process.MainModule.FileName;
                }
                catch (Win32Exception)
                {
                    path = process.ProcessName;
                }
                catch (InvalidOperationException)
                {
                    continue;
                }

                list.Add(new Dictionary<string, string>
                {
                    {"token", "Process"},
                    {"pid", "(" + process.Id + ")"},
                    {"path", path}
                });
            }

            report.Add(list);

            return true;
        }
    }
}