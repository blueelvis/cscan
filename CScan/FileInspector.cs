using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;

namespace CScan
{
    internal class FileInspector
    {
        public static string GetHash(string filePath)
        {
            var hasher = new SHA256Managed();

            using (var stream = File.OpenRead(filePath))
            {
                var hash = hasher.ComputeHash(stream);
                var hashString = BitConverter.ToString(hash).Replace("-", string.Empty);

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
    }
}