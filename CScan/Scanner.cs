using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CScan.Components;

namespace CScan
{
    class Scanner
    {
        protected string[] components = {
            "Header",
            "Integrity",
            "IEProxy",
            "Processes",
            "Environment",
            "Shell",
            "RegistryRun",
            "HiJackThis.O20",
            "HiJackThis.O21",
            "Hosts",
            "Services",
            "Drivers",
            "Disks",
            "Signatures",
            "Programs",
        };

        protected List<Component> initializedComponents = new List<Component>();

        public Scanner()
        {
            InitializeComponents();
        }

        public void Scan(ref System.Windows.Forms.RichTextBox status, string encryptionKey = null)
        {
            Report report = new Report();

            foreach (Component component in initializedComponents)
            {
                status.Text = status.Text + "Running " + component.GetType().Name + "..." + System.Environment.NewLine;

                try
                {
                    component.Run(ref report, new List<Dictionary<string, string>>());
                } catch (Exception)
                {
                    status.Text = status.Text + component.GetType().Name + " failed!" + System.Environment.NewLine;
                }
            }

            string path = report.WriteToFile(encryptionKey);

            Process.Start("notepad.exe", path);

            status.Text = status.Text + "Success!";
        }

        protected void InitializeComponents()
        {
            foreach (string component in components)
            {
                Type t = Type.GetType("CScan.Components." + component);

                Component initializedComponent = (Component) Activator.CreateInstance(t);

                initializedComponents.Add(initializedComponent);
            }
        }
    }
}
