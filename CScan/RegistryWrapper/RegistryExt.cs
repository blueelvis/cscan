using Microsoft.Win32;

namespace CScan.RegistryWrapper
{
    public static class RegistryExt
    {
        public static string toEntryString(this RegistryView view)
        {
            return view == RegistryView.Registry64 ? "(x64)" : "(x32)";
        }
    }
}