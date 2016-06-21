using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Uniscan
{
    class Report
    {
        protected List<List<KeyValuePair<string, string>>> lines = new List<List<KeyValuePair<string, string>>>();

        public void Add(List<KeyValuePair<string, string>> line)
        {
            lines.Add(line);
        }

        public string WriteToFile()
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string path = homeDirectory + "\\Desktop\\Uniscan.txt";

            System.IO.File.WriteAllText(path, ToString());

            return path;
        }

        public override string ToString()
        {
            string output = "";

            foreach(List<KeyValuePair<string, string>> line in lines)
            {
                foreach (KeyValuePair<string, string> list in line)
                {
                    if (list.Key != "token") {
                        output = output + list.Value + " ";
                    } else {
                        output = output + "[" + list.Value + "] ";
                    }
                }

                output = output + Environment.NewLine;
            }

            return output;
        }
    }
}
