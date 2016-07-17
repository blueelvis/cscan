using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CScan
{
    internal class Telemetry
    {
        private static readonly string endpoint = "https://telemetry.certly.io";

        private static readonly bool hasConnection;

        static Telemetry()
        {
            // hasConnection = CheckForInternetConnection();

            hasConnection = true;
        }

        public static void Point(string key, string value)
        {
            if (!hasConnection)
                return;

            Console.WriteLine("Submitting telemetry for " + key);

            var request = WebRequest.Create(endpoint + "/submit");

            ((HttpWebRequest) request).UserAgent = Main.name + "|" + Environment.OSVersion.Version;
            request.Method = "POST";
            request.ContentType = "application/json";

            var body = new Dictionary<string, string>
            {
                {"key", key},
                {"value", value}
            };

            var dataStream = request.GetRequestStream();
            var json = JsonConvert.SerializeObject(body);

            var jsonBytes = Encoding.ASCII.GetBytes(json);

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