using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;

namespace CScan.Commands
{
    internal class Collect : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            var directory = GetTemporaryDirectory();
            var outputPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                             @"\Desktop\Collected Files.zip";

            foreach (var file in arguments)
            {
                var fileName = FileInspector.GetHash(file) + " - " + Path.GetFileName(file);

                File.Copy(file, directory + "\\" + fileName, true);
            }

            if (File.Exists(outputPath))
                File.Delete(outputPath);

            ZipFile.CreateFromDirectory(directory, outputPath);

            list.Add(new Dictionary<string, string>
            {
                {"token", "Collect"},
                {"path", "Created successfully at " + outputPath}
            });

            return list;
        }

        private string GetTemporaryDirectory()
        {
            var tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);

            return tempDirectory;
        }
    }
}