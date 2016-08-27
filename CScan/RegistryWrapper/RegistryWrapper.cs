using Microsoft.Win32;
using System.Collections.Generic;

namespace CScan.RegistryWrapper
{
    class RegistryWrapper
    {
        public static IEnumerable<RegistryResult> QuerySubKey(RegistryHive baseKey, string subKey)
        {
            var i386 = OpenSubKey(baseKey, subKey, RegistryView.Registry32);

            if (i386 != null) {
                yield return new RegistryResult(i386, RegistryView.Registry32);
            }

            var amd64 = OpenSubKey(baseKey, subKey, RegistryView.Registry64);

            if (amd64 != null)
            {
                yield return new RegistryResult(amd64, RegistryView.Registry64);
            }
        }

        protected static RegistryKey OpenSubKey(RegistryHive hive, string subKey, RegistryView view)
        {
            RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, view);
            return baseKey.OpenSubKey(subKey);
        }
    }
}
