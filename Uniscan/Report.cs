using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan
{
    class Report
    {
        protected List<String> lines = new List<String>();

        public void Add(string line)
        {

        }

        public override string ToString()
        {
            string output = "";

            foreach(string line in lines)
            {
                output = output + line + Environment.NewLine;
            }

            return output;
        }
    }
}
