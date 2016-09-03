using System.Collections.Generic;
using System.Diagnostics;

namespace CScan.Commands
{
    internal class ResetLSP : ICommand
    {
        public List<Dictionary<string, string>> Run(List<string> arguments, List<Dictionary<string, string>> list)
        {
            var process = Process.Start("netsh", "winsock reset catalog");
            process.WaitForExit();

            list.Add(new Dictionary<string, string>
            {
                {"token", "LSP"},
                {"key", "Successfully reset LSP entries"}
            });

            return list;
        }
    }
}