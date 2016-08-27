using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Whitelist
{
    class DriverWhitelist
    {
        private static Dictionary<string, string> whitelist = new Dictionary<string, string>()
        {
            {@"C:\WINDOWS\system32\drivers\ACPI.sys", "Microsoft ACPI Driver"},
            {@"C:\WINDOWS\system32\DRIVERS\ahcache.sys", "Application Compatibility Cache"},
            {@"C:\WINDOWS\system32\drivers\bridge.sys", "Microsoft MAC Bridge"},
            {@"C:\WINDOWS\system32\Drivers\cng.sys", "CNG"},
            {@"C:\WINDOWS\system32\drivers\disk.sys", "Disk Driver"},
            {@"C:\WINDOWS\system32\DRIVERS\drmkaud.sys", "Microsoft Trusted Audio Drivers"},
            {@"C:\WINDOWS\system32\DRIVERS\fvevol.sys", "BitLocker Drive Encryption Filter Driver"},
            {@"C:\WINDOWS\system32\drivers\xboxgip.sys", "Xbox Game Input Protocol Driver"},
        };

        public static bool IsWhitelisted(string key, string value, bool keyIsSignedFile = false)
        {
            if (whitelist.ContainsKey(key) && whitelist[key] == value)
            {
                return !keyIsSignedFile || keyIsSignedFile && Authenticode.IsSigned(key);
            }

            return false;
        }
    }
}
