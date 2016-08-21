using Microsoft.Win32;

namespace CScan.RegistryWrapper
{
    class RegistryWrapper
    {
        public static RegistryResult[] QuerySubKey(RegistryHive baseKey, string subKey)
        {
            RegistryResult[] result = new RegistryResult[2];
   
            var i386 = OpenSubKey(baseKey, subKey, RegistryView.Registry32);

            if (i386 != null) {
                result[0] = new RegistryResult(i386, RegistryView.Registry32);
            }

            var amd64 = OpenSubKey(baseKey, subKey, RegistryView.Registry64);

            if (amd64 != null)
            {
                result[1] = new RegistryResult(amd64, RegistryView.Registry64);
            }

            return result;
        }

        protected static RegistryKey OpenSubKey(RegistryHive hive, string subKey, RegistryView view)
        {
            RegistryKey baseKey = RegistryKey.OpenBaseKey(hive, view);
            return baseKey.OpenSubKey(subKey);
        }
    }
}
