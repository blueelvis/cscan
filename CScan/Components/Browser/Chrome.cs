using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CScan.Components.Browser
{
    internal class Chrome : IComponent
    {
        private readonly string path = System.Environment.GetEnvironmentVariable("USERPROFILE") +
                                       @"\AppData\Local\Google\Chrome\User Data";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            if (!Directory.Exists(path))
                return;

            foreach (var directory in Directory.GetDirectories(path))
            {
                if (!Directory.Exists(directory + @"\Extensions"))
                    continue;

                foreach (var extension in Directory.GetDirectories(directory + @"\Extensions"))
                {
                    var name = Path.GetFileName(extension.TrimEnd(Path.DirectorySeparatorChar));
                    var friendly = GetFriendlyName(extension);
                    var version = GetVersion(extension);

                    if (friendly == null || friendly.StartsWith("__"))
                        continue;

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "Chrome"},
                        {"friendly", friendly},
                        {"version", "(" + version + ")" + " -"},
                        {"name", name}
                    });
                }
            }

            report.Add(list);
        }

        private string GetFriendlyName(string directory)
        {
            var directories = Directory.GetDirectories(directory);

            if (directories.Length == 0)
                return null;

            var version = directories[0];

            var manifestObj = DecodeManifest(version);

            return manifestObj.name;
        }

        private string GetVersion(string directory)
        {
            var directories = Directory.GetDirectories(directory);

            if (directories.Length == 0)
                return null;

            var version = directories[0];

            var manifestObj = DecodeManifest(version);

            return manifestObj.version;
        }

        private Manifest DecodeManifest(string directory)
        {
            var manifest = File.ReadAllText(directory + @"\manifest.json");

            var manifestObj = JsonConvert.DeserializeObject<Manifest>(manifest);

            return manifestObj;
        }

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
    }
}