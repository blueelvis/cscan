using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CScanDecrypter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void generateKey_Click(object sender, EventArgs e)
        {
            ECDiffieHellmanCng self = new ECDiffieHellmanCng();
            self.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            self.HashAlgorithm = CngAlgorithm.Sha256;

            string key = Convert.ToBase64String(self.PublicKey.ToByteArray());

            Clipboard.SetText(key);

            MessageBox.Show("Your key has been copied to the clipboard.", "Successfully Generated Key", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
