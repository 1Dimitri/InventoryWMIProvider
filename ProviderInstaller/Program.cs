using System;
using WixSharp;

namespace ProviderInstaller
{
    class Program
    {
        static void Main()
        {
            var project = new Project()
            {
                Name = "Install WMI Provider",
                UI = WUI.WixUI_ProgressOnly,
                SourceBaseDir = @"..\InventoryWMIProvider\bin\Debug",
                Dirs = new[]
           {
                new Dir(@"%ProgramFiles%\1Dimitri\WMIProvider",
                    new Assembly(@"InventoryWMIProvider.dll", true,
                        new NativeImage { Platform = NativeImagePlatform.x86}))
            }
            };

            project.GUID = new Guid("6fe30b47-2577-43ad-9095-1861ba25889b");
            Compiler.BuildMsi(project);
        }

     
    }
}