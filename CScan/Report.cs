using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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

        public string WriteToFile(string externalKey = null)
        {
            string homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            string path = homeDirectory + "\\Desktop\\" + Main.name + ".txt";

            System.IO.File.WriteAllText(path, ToString(externalKey));

            return path;
        }

        public string ToString(string externalKey = null)
        {
            string output = "";

            foreach(List<KeyValuePair<string, string>> line in lines)
            {
                foreach (KeyValuePair<string, string> list in line)
                {
                    if (list.Key != "token" && list.Value != null)
                    {
                        output = output + list.Value + " ";
                    }
                    else if (list.Value != null)
                    {
                        output = output + "[" + list.Value + "] ";
                    }
                }

                output = output + Environment.NewLine;
            }

            if (externalKey != null)
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

            AesManaged aes = new AesManaged();
            aes.Key = sharedSecret;
            aes.GenerateIV();

            ICryptoTransform transform = aes.CreateEncryptor();

            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);

            byte[] data = ASCIIEncoding.ASCII.GetBytes(blob);

            cryptoStream.Write(data, 0, data.Length);
            cryptoStream.Close();

            byte[] encryptedData = memoryStream.ToArray();

            blob = Convert.ToBase64String(encryptedData);
            blob = AddEncryptionHeaders(blob);

            memoryStream.Close();

            return blob;
        }

        protected string AddEncryptionHeaders(string output)
        {
            output = "-----BEGIN ENCRYPTED SCAN LOG-----" + Environment.NewLine 
                + "Version: 1" + Environment.NewLine
                + "Public-Key: " + publicKey + Environment.NewLine
                + Environment.NewLine + output;

            output = output + Environment.NewLine + "-----END ENCRYPTED SCAN LOG-----";

            return output;
        }
    }
}
