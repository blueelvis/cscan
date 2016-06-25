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

                bool signed = false;
                bool exists = true;

                if (File.Exists(path))
                {
                    signed = Authenticode.IsSigned(path);
                } else
                {
                    exists = false;
                }
        
                list.Add(new List<KeyValuePair<string, string>>() {
                    new KeyValuePair<string, string>("token", "Prc"),
                    new KeyValuePair<string, string>("pid", process.Id.ToString()),
                    new KeyValuePair<string, string>("path", path),
                    new KeyValuePair<string, string>("signed", exists ? !signed ? "[b](unsigned)[/b]" : null : null),
                });
            }

            report.Add(list);

            return true;
        }
    }
}
