using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Processes : Component
    {
        public bool Run(ref CScan.Report report, List<Dictionary<string, string>> list)
        {
            Process[] processes = Process.GetProcesses();

            List<Process> sortedProcesses = processes.OrderBy(process => process.Id).ToList();

            foreach (Process process in sortedProcesses)
            {
                if (process.Id == 0)
                {
                    continue;
                }

                string path;

                try
                {
                    path = process.MainModule.FileName;
                } catch (System.ComponentModel.Win32Exception)
                {
                    path = process.ProcessName;
                } catch (System.InvalidOperationException)
                {
                    continue;
                }
        
                list.Add(new Dictionary<string, string> {
                    { "token", "Prc" },
                    { "pid", process.Id.ToString() },
                    { "path", path },
                });
            }

            report.Add(list);

            return true;
        }
    }
}
