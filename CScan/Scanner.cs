using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Windows.Forms;
using CScan.Components;
using Environment = System.Environment;

namespace CScan
{
    internal class Scanner
    {
        protected string[] components =
        {
            "Header",
            "DisabledApplications",
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
            "Programs"
        };

        protected List<IComponent> initializedComponents = new List<IComponent>();

        public Scanner()
        {
            InitializeComponents();
        }

        public void Scan(ref RichTextBox status, string encryptionKey = null, bool json = false)
        {
            var report = new Report();

            foreach (var component in initializedComponents)
            {
                var componentName = component.GetType().Name;

                status.Text = status.Text + "Running " + componentName + "..." + Environment.NewLine;

                var watch = Stopwatch.StartNew();

                component.Run(ref report, new List<Dictionary<string, string>>());

                watch.Stop();

                try
                {
                    Telemetry.Point("Component." + componentName, watch.ElapsedMilliseconds.ToString());
                }
                catch (WebException)
                {
                    //
                }
            }
            
            var path = report.WriteToFile(encryptionKey, json);

            if (!json)
                Process.Start("notepad.exe", path);

            status.Text = status.Text + "Success!";
        }

        protected void InitializeComponents()
        {
            foreach (var component in components)
            {
                var t = Type.GetType("CScan.Components." + component);

                var initializedComponent = (IComponent) Activator.CreateInstance(t);

                initializedComponents.Add(initializedComponent);
            }
        }
    }
}