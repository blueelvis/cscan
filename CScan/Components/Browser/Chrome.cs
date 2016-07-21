using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using System;
using System.IO;
using Newtonsoft.Json;

namespace CScan.Components.Browser
{
    internal class Chrome : IComponent
    {
        private struct Manifest
        {
            public string name;
            public string version;

            public Manifest(string name, string version)
            {
                this.name = name;
                this.version = version;
            }
        }

        private readonly string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Local\Google\Chrome\User Data";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                if (!Directory.Exists(directory + @"\Extensions"))
                    continue;

                foreach (string extension in Directory.GetDirectories(directory + @"\Extensions"))
                {
                    string name = Path.GetFileName(extension.TrimEnd(Path.DirectorySeparatorChar));
                    string friendly = GetFriendlyName(extension);
                    string version = GetVersion(extension);

                    if (friendly.StartsWith("__"))
                        continue;

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Chrome"},
                        {"friendly", friendly},
                        {"version", "(" + version + ")" + " -"},
                        {"name", name},
                    });
                }
            }

            report.Add(list);
        }

        private string GetFriendlyName(string directory)
        {
            var version = Directory.GetDirectories(directory)[0];

            var manifestObj = DecodeManifest(version);

            return manifestObj.name;
        }

        private string GetVersion(string directory)
        {
            var version = Directory.GetDirectories(directory)[0];

            var manifestObj = DecodeManifest(version);

            return manifestObj.version;
        }

        private Manifest DecodeManifest(string directory)
        {
            var manifest = File.ReadAllText(directory + @"\manifest.json");

            var manifestObj = JsonConvert.DeserializeObject<Manifest>(manifest);

            return manifestObj;
        }
    }
}