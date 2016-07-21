using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;

namespace CScan
{
    internal class Authenticode
    {
        private static readonly string tempPath = Path.Combine(Path.GetTempPath(), "signtool.exe");

        public static bool IsSigned(string fileName, bool strict = false)
        {
            if (!File.Exists(tempPath))
            {
                File.WriteAllBytes(tempPath, Resources.signtool);
            }

            fileName = "\"" + Regex.Replace(fileName, @"(\\+)$", @"$1$1") + "\"";

            var psi = new ProcessStartInfo(tempPath, "verify /pa /a " + fileName)
            {
                CreateNoWindow = true,
                WindowStyle = ProcessWindowStyle.Hidden,
                UseShellExecute = false,
                RedirectStandardOutput = true
            };

            using (var process = Process.Start(psi))
            {
                process.WaitForExit();

                return process.ExitCode == 0;
            }
        }
    }
}