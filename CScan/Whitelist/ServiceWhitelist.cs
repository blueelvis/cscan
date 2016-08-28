using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Whitelist
{
    class ServiceWhitelist
    {
        private static Dictionary<string, string> whitelist = new Dictionary<string, string>()
        {
            {@"C:\WINDOWS\System32\alg.exe", "ALG"},
            {@"C:\WINDOWS\system32\svchost.exe -k LocalServiceNetworkRestricted", "AJRouter"},
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

