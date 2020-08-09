using System.Management.Instrumentation;

// your namespace cannot start with a digit, duh!
[assembly: WmiConfiguration(@"root\OneDimitri", HostingModel = ManagementHostingModel.NetworkService)]
namespace InventoryWMIProvider
{

    [System.ComponentModel.RunInstaller(true)]
    // public is needed otherwise you'll get attribute found, but class not accessible error
    public class MOFWMIInstaller : DefaultManagementInstaller
    {
     
    }
}
