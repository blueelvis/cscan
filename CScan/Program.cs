using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
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
            if (Environment.Is64BitOperatingSystem != Environment.Is64BitProcess)
            {
                MessageBox.Show("You are using the wrong version of CScan for your architecture. Please download the " + (Environment.Is64BitProcess ? "x86" : "x64") + " version of CScan.",
                    "Architecture Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Environment.Exit(1);
            }

            SafeHandle handle = Process.GetCurrentProcess().SafeHandle;
            ProcessSecurity manager = new ProcessSecurity(handle);

            IdentityReference identityReference = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
            int accessMask = 0x0001;
            InheritanceFlags inheritanceFlags = InheritanceFlags.None;
            PropagationFlags propagationFlags = PropagationFlags.None;
            AccessControlType type = AccessControlType.Deny;
            
            AccessRule rule = manager.AccessRuleFactory(identityReference, accessMask, true, inheritanceFlags, propagationFlags, type);
            manager.AddAccessRule((ProcessAccessRule) rule);
            manager.SaveChanges(handle);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }
    }
}