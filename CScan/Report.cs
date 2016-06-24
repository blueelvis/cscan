using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace CScan
{
    class Report
    {
        protected List<List<KeyValuePair<string, string>>> lines = new List<List<KeyValuePair<string, string>>>();

        protected string publicKey;

        public void Add(List<List<KeyValuePair<string, string>>> newLines)
        {
            foreach (List<KeyValuePair<string, string>> line in newLines)
            {
                lines.Add(line);
            }

            // Add an extra empty line between sections.
            lines.Add(new List<KeyValuePair<string, string>>());
        }

        public string WriteToFile()
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string path = homeDirectory + "\\Desktop\\" + Main.name + ".txt";

            System.IO.File.WriteAllText(path, ToString());

            return path;
        }

        public string ToString(bool shouldEncrypt = false, string externalKey = null)
        {
            string output = "";

            foreach(List<KeyValuePair<string, string>> line in lines)
            {
                foreach (KeyValuePair<string, string> list in line)
                {
                    if (list.Key != "token")
                    {
                        output = output + list.Value + " ";
                    }
                    else
                    {
                        output = output + "[" + list.Value + "] ";
                    }
                }

                output = output + Environment.NewLine;
            }

            if (shouldEncrypt)
            {
                output = Encrypt(output, externalKey);
            }

            return output;
        }

        public string ToJson()
        {
            return new JavaScriptSerializer().Serialize(lines);
        }

        protected string Encrypt(string blob, string externalKey)
        {
            ECDiffieHellmanCng self = new ECDiffieHellmanCng();
            self.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            self.HashAlgorithm = CngAlgorithm.Sha256;

            publicKey = Convert.ToBase64String(self.PublicKey.ToByteArray());

            byte[] externalKeyBytes = Convert.FromBase64String(externalKey);

            ECDiffieHellmanPublicKey externalKeyObject = ECDiffieHellmanCngPublicKey.FromByteArray(externalKeyBytes, CngKeyBlobFormat.GenericPublicBlob);

            byte[] sharedSecret = self.DeriveKeyMaterial(externalKeyObject);

            Console.WriteLine(sharedSecret.ToString());

            return AddEncryptionHeaders(blob);
        }

        protected string AddEncryptionHeaders(string output)
        {
            output = "-----BEGIN ENCRYPTED SCAN LOG-----" + Environment.NewLine 
                + "Version: 1" + Environment.NewLine
                + "Public-Key: " + publicKey + Environment.NewLine
                + output;

            output = output + Environment.NewLine + "-----END ENCRYPTED SCAN LOG-----";

            return output;
        }
    }
}
