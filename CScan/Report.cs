using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CScan
{
    class Report
    {
        protected List<List<KeyValuePair<string, string>>> lines = new List<List<KeyValuePair<string, string>>>();

        public void Add(List<List<KeyValuePair<string, string>>> newLines)
        {
            foreach (List<KeyValuePair<string, string>> line in newLines)
            {
                lines.Add(line);
            }

            // Add an extra empty line between sections.
            lines.Add(new List<KeyValuePair<string, string>>());
        }

        public string WriteToFile()
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string path = homeDirectory + "\\Desktop\\" + Main.name + ".txt";

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

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(lines);
        }
    }
}
