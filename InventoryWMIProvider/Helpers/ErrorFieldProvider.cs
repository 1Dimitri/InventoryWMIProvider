using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

namespace InventoryWMIProvider.Helpers
{
    abstract class ErrorFieldProvider

    {
        public ErrorFieldProvider() 
        {
            InvalidFields = new List<string>();
        }
        [ManagementProbe]
        public bool HasErrors { get; private set; }

        [ManagementProbe]
        public List<String> InvalidFields { get; private set; }
    }
}
