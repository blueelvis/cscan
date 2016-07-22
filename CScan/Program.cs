using System;
using System.Windows.Forms;

namespace CScan
{
    internal static class Program
    {
        /// <summary>
        ///     The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            // If the binary is a different architecture than the system.
            if (Environment.Is64BitOperatingSystem && !Environment.Is64BitProcess || !System.Environment.Is64BitOperatingSystem && Environment.Is64BitProcess)
            {
                MessageBox.Show("You are using the wrong version of CScan for your architecture. Please download the " + (Environment.Is64BitProcess ? "x86" : "x64") + " version of CScan.",
                    "Architecture Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(1);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}