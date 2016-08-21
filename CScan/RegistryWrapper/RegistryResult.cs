using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.RegistryWrapper
{
    struct RegistryResult
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