using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CScan
{
    class Telemetry
    {
        private static string endpoint = "https://telemetry.certly.io";

        private static bool hasConnection;

        static Telemetry()
        {
            hasConnection = CheckForInternetConnection();
        }

        public static void Point(string key, string value)
        {
            if (!hasConnection)
                return;

            WebRequest request = WebRequest.Create(endpoint + "/submit");

            ((HttpWebRequest)request).UserAgent = Main.name + "|" + System.Environment.OSVersion.Version;
            request.Method = "POST";
            request.ContentType = "application/json";

            Dictionary<string, string> body = new Dictionary<string, string>() {
                { "key", key },
                { "value", value },
            };

            Stream dataStream = request.GetRequestStream();
            string json = JsonConvert.SerializeObject(body);

            byte[] jsonBytes = Encoding.ASCII.GetBytes(json);

            dataStream.Write(jsonBytes, 0, jsonBytes.Length);
            dataStream.Close();

            Task response = request.GetResponseAsync();
            response.Start();
        }

        private static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(endpoint + "/connectivity"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
