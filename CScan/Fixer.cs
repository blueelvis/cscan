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
            {"run", "RunCommand"}
        };

        private RichTextBox status;

        private string currentSection;

        private List<string> lineBuffer = new List<string>();

        private List<List<List<Dictionary<string, string>>>> results = new List<List<List<Dictionary<string, string>>>>();

        public void Fix(ref RichTextBox richTextBox, string fileName = null)
        {
            status = richTextBox;

            if (fileName == null)
            {
                fileName = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + @"\Desktop\fix.txt";
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

            if (currentSection != null && lineBuffer.Count > 0)
                RunSection();
        }

        public override string ToString()
        {
            var s = "";

            foreach (List<List<Dictionary<string, string>>> section in results)
            {
                foreach (List<Dictionary<string, string>> line in section)
                {
                    //
                }
            }

            return s;
        }

        private void ProcessLine(string line)
        {
            if (line.Substring(0, 1) == ":")
            {
                HandleSection(line.Substring(1).ToLower());
            } else
            {
                HandleEntry(line);
            }
        }

        private void HandleSection(string command)
        {
            if (currentSection != null && lineBuffer.Count > 0)
                RunSection();

            currentSection = command;
        }

        private void RunSection()
        {
            status.Text = status.Text + "Processing " + currentSection.Substring(0, 1).ToUpper() + currentSection.Substring(1) + "." + Environment.NewLine;

            var command = ResolveCommand(commands[currentSection]);
            results.Add(command.Run(lineBuffer, new List<List<Dictionary<string, string>>>()));

            currentSection = null;
            lineBuffer.Clear();
        }

        private void HandleEntry(string line)
        {
            if (currentSection == null)
                throw new InvalidDataException("An entry was passed outside of a section.");

            lineBuffer.Add(line);
        }

        private ICommand ResolveCommand(string command)
        {
            var t = Type.GetType("CScan.Commands." + command);

            return (ICommand) Activator.CreateInstance(t);
        }
    }
}