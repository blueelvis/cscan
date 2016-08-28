using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace CScan.Commands
{
    internal class Files : ICommand
    {
        private static string destinationFolder = Path.GetPathRoot(Environment.SystemDirectory) + @"CScan\Backup";

        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            Directory.CreateDirectory(destinationFolder);

            foreach (var file in arguments)
            {
                if (!File.Exists(file))
                {
                    list.Add(new Dictionary<string, string>()
                    {
                        {"token", "File"},
                        {"err", file + " does not exist"},
                    });
                    continue;
                }

                var encoding  = new ASCIIEncoding();
                
                var destFileName = FileInspector.GetHash(file) + " - " + Convert.ToBase64String(encoding.GetBytes(file));
                var destFilePath = destinationFolder + @"\" + destFileName;

                if (File.Exists(destFilePath))
                {
                    File.Delete(file);
                    list.Add(new Dictionary<string, string>()
                    {
                        {"token", "File"},
                        {"success", "Moved (by deletion) " + file},
                    });
                    continue;
                }

                try
                {
                    File.Move(file, destFilePath);
                }
                catch
                {
                    list.Add(new Dictionary<string, string>()
                    {
                        {"token", "File"},
                        {"err", "Failed to move " + file},
                    });
                    continue;
                }

                list.Add(new Dictionary<string, string>()
                {
                    {"token", "File"},
                    {"success", "Successfully moved " + file},
                });
            }

            return list;
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);

            return tempDirectory;
        }
    }
}