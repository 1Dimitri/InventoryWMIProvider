using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;

[assembly: WmiConfiguration(@"root\1Dimitri", HostingModel = ManagementHostingModel.NetworkService)]
namespace InventoryWMIProvider
{

    [System.ComponentModel.RunInstaller(true)]
    class Installer : DefaultManagementInstaller
    {
        static void Main(string[] args)
        {
        }
    }
}
