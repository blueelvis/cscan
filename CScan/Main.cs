using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CScan
{
    public partial class Main : Form
    {
        public const string name = "CScan";

        public const string version = "0.0.0-dev";

        public Main()
        {
            InitializeComponent();

            Process.EnterDebugMode();
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;

            MessageBox.Show(name + " is distributed by Certly Inc under the Apache 2.0 license (the \"License\")." + Environment.NewLine + Environment.NewLine + "Unless required by applicable law or agreed to in writing, software distributed under the License is distributed on an \"AS IS\" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.");
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();

            scanner.Scan(ref statusText, encryptionKey.Text == "Optional Encryption Key" ? null : encryptionKey.Text);
        }

        private void Fix_Click(object sender, EventArgs e)
        {
            Fixer fixer = new Fixer();

            fixer.Fix(ref statusText);
        }
    }
}