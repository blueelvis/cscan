using System;
using System.Collections.Generic;
using System.IO;

namespace CScan.Components
{
    internal class Files : IComponent
    {
        private readonly string[] directories =
        {
            @"C:\Users",
            @"C:\Program Files",
            @"C:\Program Files (x86)"
        };

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var directory in directories)
            {
                if (!Directory.Exists(directory))
                    continue;

                foreach (var file in SafeFileEnumerator.EnumerateFiles(directory, "*.exe", SearchOption.AllDirectories))
                {
                    if (file.Length > 248)
                        continue;

                    var fileDate = FileInspector.GetDate(file);

                    if ((fileDate - DateTime.Now).TotalDays > 14)
                        continue;

                    try
                    {
                        list.Add(new Dictionary<string, string>
                        {
                            {"token", "File"},
                            {"hash", "[" + FileInspector.GetHash(file)},
                            {"date", fileDate.ToString("MM-dd-yyyy") + "]"},
                            {"publisher", "(" + FileInspector.GetPublisher(file) + ")"},
                            {"path", file}
                        });
                    }
                    catch (IOException)
                    {
                        list.Add(new Dictionary<string, string>
                        {
                            {"token", "File"},
                            {"date", "[" + fileDate.ToString("MM-dd-yyyy") + "]"},
                            {"path", file}
                        });
                    }
                }
            }

            report.Add(list);
        }
    }
}