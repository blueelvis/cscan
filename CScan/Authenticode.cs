using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CScan
{
    class Authenticode
    {
        private static string tempPath = Path.Combine(Path.GetTempPath(), "signtool.exe");

        public static bool IsSigned(string fileName, bool strict = false)
        {
            if (!File.Exists(tempPath))
            {
                File.WriteAllBytes(tempPath, CScan.Resources.signtool);
            }

            fileName = "\"" + Regex.Replace(fileName, @"(\\+)$", @"$1$1") + "\"";

            Process process = Process.Start(tempPath, "verify /pa " + fileName);
            process.WaitForExit();

            return process.ExitCode == 0;
        }
    }
}
