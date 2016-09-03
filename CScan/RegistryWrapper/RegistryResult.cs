using Microsoft.Win32;

namespace CScan.RegistryWrapper
{
    internal struct RegistryResult
    {
        public RegistryKey key;
        public RegistryView view;

        public RegistryResult(RegistryKey key, RegistryView view)
        {
            this.key = key;
            this.view = view;
        }
    }
}