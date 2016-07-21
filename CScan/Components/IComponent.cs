using System.Collections.Generic;

namespace CScan.Components
{
    internal interface IComponent
    {
        void Run(ref Report report, List<Dictionary<string, string>> line);
    }
}