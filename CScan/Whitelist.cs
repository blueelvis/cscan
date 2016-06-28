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

        private static Dictionary<string, bool> hashCache = new Dictionary<string, bool>();

        private static SHA256 hasher = SHA256Managed.Create();

        public bool IsFileWhitelisted(string path)
        {
            if (hashCache.ContainsKey(path))
            {
                return hashCache[path];
            }

            FileStream file = OpenFile(path);
            string hash = hasher.ComputeHash(file).ToString();

            bool result = hashes.Contains(hash);

            hashCache[path] = result;

            return result;
        }

        private FileStream OpenFile(string path)
        {
            return new FileStream(path, FileMode.Open);
        }
    }
}
