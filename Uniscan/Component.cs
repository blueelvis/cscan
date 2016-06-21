using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan.Components
{
        interface Component
        {
            bool ShouldRun();

            bool Run(ref System.Windows.Forms.TextBox status);
        }
}
