using System.Collections.Generic;

namespace CScan.Whitelist
{
    internal class ServiceWhitelist
    {
        private static readonly Dictionary<string, string> whitelist = new Dictionary<string, string>
        {
            {@"C:\WINDOWS\System32\alg.exe", "ALG"},
            {@"C:\WINDOWS\system32\svchost.exe -k LocalServiceNetworkRestricted", "AJRouter"}
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