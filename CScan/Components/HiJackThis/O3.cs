using System.Collections.Generic;
using CScan.RegistryWrapper;
using Microsoft.Win32;

namespace CScan.Components.HiJackThis
{
    internal class O3 : IComponent
    {
        protected string ToolbarKey = @"SOFTWARE\Microsoft\Internet Explorer\Toolbar";

        public void Run(ref Report report, List<Dictionary<string, string>> list)
        {
            foreach (var result in RegistryWrapper.RegistryWrapper.QuerySubKey(RegistryHive.LocalMachine, ToolbarKey))
            {
                foreach (var toolbarClsid in result.key.GetValueNames())
                {
                    var companyName = Clsid.GetName(toolbarClsid, result.view);
                    var filePath = Clsid.GetFile(toolbarClsid, result.view);

                    list.Add(new Dictionary<string, string>
                    {
                        {"Token", "O3"},
                        {"regview", result.view.toEntryString()},
                        {"company", companyName ?? "()"},
                        {"clsid", toolbarClsid},
                        {"path", filePath ?? "<File not found>"}
                    });
                }
            }


            report.Add(list);
        }
    }
}