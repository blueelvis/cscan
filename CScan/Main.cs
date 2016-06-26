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
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();

            scanner.Scan(ref statusText, encryptionKey.Text == "Optional Encryption Key" ? null : encryptionKey.Text);
        }
    }
}