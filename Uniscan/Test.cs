using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan.Components
{
    class Test : Component
    {
        public bool Run(ref CScan.Report report)
        {
            List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>() {
                new KeyValuePair<string, string>("token", "TST"),
                new KeyValuePair<string, string>("data", "abcDef"),
                new KeyValuePair<string, string>("data", "ghiJkl"),
            };

            report.Add(list);

            return true;
        }
    }
}
