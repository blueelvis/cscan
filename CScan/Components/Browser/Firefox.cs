using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using Microsoft.Win32;
using System;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CScan.Components.Browser
{
    internal class Firefox : IComponent
    {
        private readonly string path = System.Environment.GetEnvironmentVariable("USERPROFILE") + @"\AppData\Roaming\Mozilla\Firefox";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            if (!Directory.Exists(path))
                return;

            foreach (string profile in Directory.GetDirectories(path + @"\Profiles"))
            {
                if (!File.Exists(profile + @"\extensions.json"))
                    continue;

                var manifest = DecodeManifest(profile);
                var addons = manifest.GetValue("addons");

                foreach (JObject addon in addons)
                {
                    var locale = (JObject)addon.GetValue("defaultLocale");

                    var id = (string) addon.GetValue("id");
                    var version = (string) addon.GetValue("version");
                    var name = (string) locale.GetValue("name");

                    list.Add(new Dictionary<string, string>
                    {
                        {"token", "FF"},
                        {"id", id + " -"},
                        {"name", name},
                        {"version", "(" + version + ")"}
                    });
                }
            }

            report.Add(list);
        }

        private JObject DecodeManifest(string directory)
        {
            var manifest = File.ReadAllText(directory + @"\extensions.json");

            var manifestObj = JObject.Parse(manifest);

            return manifestObj;
        }
    }
}