using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CScan
{
    internal class Whitelist
    {
        private static readonly string[] hashes =
        {
        };

        private static readonly Dictionary<string, bool> hashCache = new Dictionary<string, bool>();

        private static readonly SHA256 hasher = SHA256.Create();

        public bool IsFileWhitelisted(string path)
        {
            if (hashCache.ContainsKey(path))
            {
                return hashCache[path];
            }

            var file = OpenFile(path);
            var hash = hasher.ComputeHash(file).ToString();

            var result = hashes.Contains(hash);

            hashCache[path] = result;

            return result;
        }

        private FileStream OpenFile(string path)
        {
            return new FileStream(path, FileMode.Open);
        }
    }
}