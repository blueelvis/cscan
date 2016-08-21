using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Registry
{
    enum RegistryKeyArchitecture
    {
        amd64,
        i386,
    };

    struct RegistryResult
    {
        public RegistryKey key;
        public RegistryKeyArchitecture architecture;

        public RegistryResult(RegistryKey key, RegistryKeyArchitecture architecture)
        {
            this.key = key;
            this.architecture = architecture;
        }
    }
}
