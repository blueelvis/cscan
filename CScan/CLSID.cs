using Microsoft.Win32;

namespace CScan
{
    internal class Clsid
    {
        public static string GetName(string clsid)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Classes\CLSID\" + clsid))
            {
                return (string) key?.GetValue(null);
            }
        }

        public static string GetFile(string clsid)
        {
            using (var key = Registry.LocalMachine.OpenSubKey(@"Software\Classes\CLSID\" + clsid + @"\InprocServer32"))
            {
                return (string) key?.GetValue(null);
            }
        }
    }
}