using CScan.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan
{
    class Fixer
    {
        public static string[] commands =
        {
            "RunCommand",
        };

        private System.Windows.Forms.RichTextBox status;

        public void ProcessFix(ref System.Windows.Forms.RichTextBox richTextBox, string fileName = null)
        {
            status = richTextBox;

            if (fileName == null)
            {
                fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\" + "fix.txt";
            }

            string[] contents = File.ReadAllLines(fileName);

            foreach (string line in contents)
            {
                ProcessLine(line);
            }
        }

        private dynamic ResolveCommand(string command)
        {
            Type t = Type.GetType("CScan.Commands." + command);

            return (Command)Activator.CreateInstance(t);
        }

        private void ProcessLine(string line)
        {
            //
        }
    }
}
