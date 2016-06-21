using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
        interface Component
        {
            bool Run(ref CScan.Report report);
        }
}
