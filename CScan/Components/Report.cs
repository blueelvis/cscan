using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Script.Serialization;

namespace CScan
{
    internal class Report
    {
        protected List<Dictionary<string, string>> lines = new List<Dictionary<string, string>>();

        protected string publicKey;

        public void Add(List<Dictionary<string, string>> newLines)
        {
            foreach (var line in newLines)
            {
                lines.Add(line);
            }

            // Add an extra empty line between sections.
            lines.Add(new Dictionary<string, string>());
        }

        public string WriteToFile(string externalKey = null)
        {
            var homeDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

            var path = homeDirectory + "\\Desktop\\" + Main.name + ".txt";

            File.WriteAllText(path, ToString(externalKey));

            return path;
        }

        public string ToString(string externalKey = null)
        {
            var output = "";

            foreach (var line in lines)
            {
                foreach (var list in line)
                {
                    if (list.Key != "token" && list.Value != null)
                    {
                        output = output + list.Value + " ";
                    }
                    else if (list.Value != null)
                    {
                        output = output + list.Value + ": ";
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
            var self = new ECDiffieHellmanCng();
            self.KeyDerivationFunction = ECDiffieHellmanKeyDerivationFunction.Hash;
            self.HashAlgorithm = CngAlgorithm.Sha256;

            publicKey = Convert.ToBase64String(self.PublicKey.ToByteArray());

            var externalKeyBytes = Convert.FromBase64String(externalKey);

            var externalKeyObject = ECDiffieHellmanCngPublicKey.FromByteArray(externalKeyBytes,
                CngKeyBlobFormat.GenericPublicBlob);

            var sharedSecret = self.DeriveKeyMaterial(externalKeyObject);

            var aes = new AesManaged();
            aes.Key = sharedSecret;
            aes.GenerateIV();

            var transform = aes.CreateEncryptor();

            using (var memoryStream = new MemoryStream())
            {
                var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);

                var data = Encoding.ASCII.GetBytes(blob);

                cryptoStream.Write(data, 0, data.Length);
                cryptoStream.Close();

                var encryptedData = memoryStream.ToArray();

                blob = Convert.ToBase64String(encryptedData);

                self.Dispose();
                aes.Dispose();

                return AddEncryptionHeaders(blob);
            }
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