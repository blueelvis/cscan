using System.Collections.Generic;

namespace CScan.Components
{
    internal interface Component
    {
        bool Run(ref Report report, List<Dictionary<string, string>> line);
    }
}