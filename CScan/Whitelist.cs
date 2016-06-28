using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CScan
{
    class Whitelist
    {
        private static string[] hashes = {
            
        };

        private static SHA256 hasher = SHA256Managed.Create();

        public bool IsFileWhitelisted(string path)
        {
            FileStream file = OpenFile(path);
            string hash = hasher.ComputeHash(file).ToString();

            return hashes.Contains(hash);
        }

        private FileStream OpenFile(string path)
        {
            return new FileStream(path, FileMode.Open);
        }
    }
}
