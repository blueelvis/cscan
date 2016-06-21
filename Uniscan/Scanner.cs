using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniscan.Components;

namespace Uniscan
{
    class Scanner
    {
        protected string[] components = {
            "Test",
        };

        protected List<Component> initializedComponents = new List<Component>();

        public Scanner()
        {
            InitializeComponents();
        }

        public void Scan(ref System.Windows.Forms.TextBox status)
        {
            foreach (Component component in initializedComponents)
            {
                if (component.ShouldRun())
                {
                    Console.WriteLine("Running...");

                    component.Run(ref status);

                    Console.WriteLine("Ran!");
                }
            }

            status.Text = "Success!";
        }

        protected void InitializeComponents()
        {
            foreach (string component in components)
            {
                Type t = Type.GetType("Uniscan.Components." + component);

                Component initializedComponent = (Component) Activator.CreateInstance(t);

                initializedComponents.Add(initializedComponent);
            }
        }
    }
}
