using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using System;
using System.IO;

namespace CScan.Components
{
    internal class Files : IComponent
    {
        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (string file in FileInspector.GetFiles(System.Environment.GetEnvironmentVariable("SYSTEMDRIVE") + "\\"))
            {
                if (file.Length > 248 || !file.EndsWith(".exe") && !file.EndsWith(".dll"))
                    continue;

                DateTime fileDate = FileInspector.GetDate(file);

                if ((fileDate - DateTime.Now).TotalDays > 14)
                    continue;

                try
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "File"},
                        {"date", "[" + fileDate.ToString("MM-dd-yyyy")},
                        {"hash", FileInspector.GetHash(file) + "]"},
                        {"publisher", "(" + FileInspector.GetPublisher(file) + ")"},
                        {"path", file}
                    });
                } catch (IOException)
                {
                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "File"},
                        {"date", "[" + fileDate.ToString("MM-dd-yyyy") + "]"},
                        {"path", file}
                    });
                }
            }

            report.Add(list);
        }
    }
}