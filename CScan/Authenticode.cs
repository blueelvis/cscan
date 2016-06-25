using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CScan
{
    class Authenticode
    {
        public static bool IsSigned(string fileName)
        {
            X509Certificate cert = null;

            try
            {
                cert = X509Certificate.CreateFromSignedFile(fileName);
            }
            catch (CryptographicException)
            {
                return false;
            }

            X509Certificate2 v2Cert = new X509Certificate2(cert);

            return v2Cert.Verify();
        }
    }
}
