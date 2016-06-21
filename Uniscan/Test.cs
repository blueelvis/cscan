using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan.Components
{
    class Test : Component
    {
        public bool Run(ref Uniscan.Report report)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();

            list.Add(new KeyValuePair<string, string>("token", "TST"));
            list.Add(new KeyValuePair<string, string>("data", "abcDef"));

            report.Add(list);

            return true;
        }
    }
}
