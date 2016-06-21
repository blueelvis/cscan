using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan.Components
{
        interface Component
        {
            bool Run(ref Uniscan.Report report);
        }
}
