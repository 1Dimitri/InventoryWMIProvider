using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management.Instrumentation;

namespace InventoryWMIProvider
{
        [ManagementEntity(Singleton = true)]
        [ManagementQualifier("Description", Value = "returns the version of the WMI Provider.")]
        public class WMIProviderVersion
        {
            private string _AssemblyVersionAsString;
            private Version _AssemblyVersion;
            [ManagementBind]
            public WMIProviderVersion()
            {
                System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
                _AssemblyVersion = assembly.GetName().Version;
                _AssemblyVersionAsString = _AssemblyVersion.ToString();

            }

            [ManagementProbe]
            [ManagementQualifier("Description",Value = "returns the version of the provider as string.")]

            public string WmiProviderVersionAsString => _AssemblyVersionAsString;

            [ManagementProbe]
            [ManagementQualifier("Description",Value = "returns the major version")]
            public int MajorVersion => _AssemblyVersion.Major;

            [ManagementProbe]
            [ManagementQualifier("Description",Value = "returns the build version")]
            public int Build => _AssemblyVersion.Build;

            [ManagementProbe]
            [ManagementQualifier("Description",
                   Value = "returns the minor version")]
            public int MinorVersion => _AssemblyVersion.Minor;

            [ManagementProbe]
            [ManagementQualifier("Description",
                   Value = "returns the revision version")]
            public int Revision => _AssemblyVersion.Revision;
    }
}
