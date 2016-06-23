using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Processes : Component
    {
        public bool Run(ref CScan.Report report, List<List<KeyValuePair<string, string>>> list)
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
        
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Prc"),
                    new KeyValuePair<string, string>("pid", process.Id.ToString()),
                    new KeyValuePair<string, string>("path", path),
                });
            }

            report.Add(list);

            return true;
        }
    }
}
