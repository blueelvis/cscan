using CScan.Commands;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void ProcessLine(string line)
        {
            string[] parts = line.Split(' ');

            string command = line[0].ToString();

            if (!commands.Contains(command))
            {
                ExecuteProcess(String.Join(" ", parts));
                return;
            }

            Command resolvedCommand = ResolveCommand(command);

            string[] arguments = parts.Where((source, index) => index != 0).ToArray();

            resolvedCommand.Run(new List<Dictionary<string, string>>(), arguments);
        }

        private void ExecuteProcess(string command)
        {
            Process.Start("cmd", "/c " + command);
        }

        private Command ResolveCommand(string command)
        {
            Type t = Type.GetType("CScan.Commands." + command);

            return (Command)Activator.CreateInstance(t);
        }
    }
}
