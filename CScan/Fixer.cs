using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using CScan.Commands;

namespace CScan
{
    internal class Fixer
    {
        public static Dictionary<string, string> commands = new Dictionary<string, string>
        {
            // Command => Class
            {"Run", "RunCommand"}
        };

        private RichTextBox status;

        public void Fix(ref RichTextBox richTextBox, string fileName = null)
        {
            status = richTextBox;

            if (fileName == null)
            {
                fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Desktop\\" + "fix.txt";
            }

            if (!File.Exists(fileName))
            {
                richTextBox.Text = "Fix file does not exist!";
                return;
            }

            var contents = File.ReadAllLines(fileName);

            foreach (var line in contents)
            {
                var cleanLine = line.Trim();

                if (cleanLine == "" || cleanLine.Substring(0, 1) == "#")
                    continue;

                ProcessLine(cleanLine);
            }
        }

        private void ProcessLine(string line)
        {
            var parts = line.Split(' ');

            var command = line[0].ToString();

            if (!commands.ContainsKey(command))
            {
                ExecuteProcess(string.Join(" ", parts));
                return;
            }

            var resolvedCommand = ResolveCommand(commands[command]);

            var arguments = parts.Where((source, index) => index != 0).ToArray();

            resolvedCommand.Run(new List<Dictionary<string, string>>(), arguments);
        }

        private void ExecuteProcess(string command)
        {
            Process.Start("cmd", "/c " + command);
        }

        private Command ResolveCommand(string command)
        {
            var t = Type.GetType("CScan.Commands." + command);

            return (Command) Activator.CreateInstance(t);
        }
    }
}