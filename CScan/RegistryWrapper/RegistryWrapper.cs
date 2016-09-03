using System;
using System.Collections.Generic;
using Microsoft.Win32;

namespace CScan.RegistryWrapper
{
    internal class RegistryWrapper
    {
        public static IEnumerable<RegistryResult> QuerySubKey(RegistryHive baseKey, string subKey)
        {
            if (baseKey != RegistryHive.LocalMachine)
            {
                var i386 = OpenSubKey(baseKey, subKey, RegistryView.Registry32);

                if (i386 != null)
                {
                    yield return new RegistryResult(i386, RegistryView.Registry32);
                }
            }

            var amd64 = OpenSubKey(baseKey, subKey, RegistryView.Registry64);

            if (amd64 != null)
            {
                yield return new RegistryResult(amd64, RegistryView.Registry64);
            }
        }

        protected static RegistryKey OpenSubKey(RegistryHive hive, string subKey, RegistryView view)
        {
            var baseKey = RegistryKey.OpenBaseKey(hive, view);
            return baseKey.OpenSubKey(subKey);
        }
    }
}