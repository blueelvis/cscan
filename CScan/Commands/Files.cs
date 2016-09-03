using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Runtime.InteropServices;
using System.Text;

namespace CScan.Commands
{
    internal class Files : ICommand
    {
        private static string destinationFolder = Path.GetPathRoot(Environment.SystemDirectory) + @"CScan\Backup";

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        static extern bool MoveFileEx(string lpExistingFileName, string lpNewFileName,
        MoveFileFlags dwFlags);

        [Flags]
        enum MoveFileFlags
        {
            MOVEFILE_REPLACE_EXISTING = 0x00000001,
            MOVEFILE_COPY_ALLOWED = 0x00000002,
            MOVEFILE_DELAY_UNTIL_REBOOT = 0x00000004,
            MOVEFILE_WRITE_THROUGH = 0x00000008,
            MOVEFILE_CREATE_HARDLINK = 0x00000010,
            MOVEFILE_FAIL_IF_NOT_TRACKABLE = 0x00000020
        }

        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            Directory.CreateDirectory(destinationFolder);

            foreach (var file in arguments)
            {
                if (!File.Exists(file) && !Directory.Exists(file))
                {
                    list.Add(new Dictionary<string, string>()
                    {
                        {"token", "File"},
                        {"err", file + " does not exist"},
                    });
                    continue;
                }

                var encoding  = new ASCIIEncoding();

                string destFileName;

                if (!Directory.Exists(file))
                {
                    destFileName = FileInspector.GetHash(file) + " - " + Convert.ToBase64String(encoding.GetBytes(file));
                }
                else
                {
                    destFileName = "Directory - " + Convert.ToBase64String(encoding.GetBytes(file));
                }

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

                MoveFile(ref list, file, destFilePath);
            }

            return list;
        }

        private string GetTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);

            return tempDirectory;
        }

        private void MoveFile(ref List<Dictionary<string, string>> list, string file, string destFilePath)
        {
            MoveFileEx(file, destFilePath, MoveFileFlags.MOVEFILE_DELAY_UNTIL_REBOOT);

            list.Add(new Dictionary<string, string>()
            {
                {"token", "File"},
                {"success", "Set to move on reboot: " + file},
                {"reboot_required", "true"},
            });
        }
    }
}