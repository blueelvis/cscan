using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CScan
{
    internal class FileInspector
    {
        public static string GetHash(string filePath)
        {
            var hasher = new SHA256Managed();

            using (FileStream stream = File.OpenRead(filePath))
            {
                byte[] hash = hasher.ComputeHash(stream);
                string hashString = BitConverter.ToString(hash).Replace("-", string.Empty);

                return hashString;
            }
        }

        public static DateTime GetDate(string filePath)
        {
            return File.GetCreationTime(filePath);
        }

        public static string GetPublisher(string filePath)
        {
            var versionInfo = FileVersionInfo.GetVersionInfo(filePath);
            var companyName = versionInfo.CompanyName;

            return companyName;
        }

        public static IEnumerable<string> GetFiles(string path)
        {
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(path);
            while (queue.Count > 0)
            {
                path = queue.Dequeue();
                try
                {
                    foreach (string subDir in Directory.GetDirectories(path))
                    {
                        queue.Enqueue(subDir);
                    }
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                string[] files = null;
                try
                {
                    files = Directory.GetFiles(path);
                }
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex);
                }
                if (files != null)
                {
                    for (int i = 0; i < files.Length; i++)
                    {
                        yield return files[i];
                    }
                }
            }
        }
    }
}
