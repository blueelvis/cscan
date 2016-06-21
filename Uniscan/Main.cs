using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Uniscan
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void Scan_Click(object sender, EventArgs e)
        {
            Scanner scanner = new Scanner();

            scanner.Scan(ref statusText);
        }
    }
}
