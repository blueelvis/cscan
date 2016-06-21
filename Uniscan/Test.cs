using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan.Components
{
    class Test : Component
    {
        public bool ShouldRun()
        {
            return true;
        }

        public bool Run(ref System.Windows.Forms.TextBox status)
        {
            return true;
        }
    }
}
