using Microsoft.Deployment.WindowsInstaller;
using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using WixSharp;
using WixSharp.CommonTasks;

namespace ProviderInstaller
{
    class MSIMaker
    {
        static void Main()
        {
#if DEBUG
            string srcDir = @"..\InventoryWMIProvider\bin\Debug";
#else
            string srcDir = @"..\InventoryWMIProvider\bin\Release";
#endif
            string dllFile = Path.Combine(srcDir, "InventoryWMIProvider.dll");
            System.Reflection.Assembly asm = System.Reflection.Assembly.LoadFrom(dllFile);
            string strongname = asm.GetName().FullName;
            Version asmver = asm.GetName().Version;

            var project = new Project()
            {
                OutFileName = $"InventoryWMIProviderSetup_{asmver}",
                OutDir="output",
                Name = "InventoryWMIProvider",
                UI = WUI.WixUI_ProgressOnly,
                Version = asmver, // or  project.SetVersionFromFile(dllFile);

                Dirs = new[]
                  {
                    new Dir(@"%ProgramFiles%\1Dimitri\InventoryWMIProvider",

                    // This does the GAC registration for us
                    new Assembly(dllFile, true,
                        new NativeImage { Platform = NativeImagePlatform.all }))
                },
                Properties = new Property[]
                {
                    // Storing the StrongName for our custom actions which will call InstallUtil /AssemblyName
                    new Property("StrongName",strongname)
                },
                Actions = new[] {
                new ElevatedManagedAction(CustomActions.InstallService, Return.check, When.After, Step.MsiPublishAssemblies, Condition.NOT_Installed) {
                    UsesProperties ="StrongName"
                },
                new ElevatedManagedAction(CustomActions.UnInstallService, Return.check, When.Before, Step.MsiUnpublishAssemblies, Condition.BeingUninstalled)
                {
                    UsesProperties = "StrongName"
                }
                }
            };


            project.GUID = new Guid("9FF27AF2-20F3-4A76-B622-30FE14737D34");
            Compiler.BuildMsi(project);
        }


    }

    public class CustomActions
    {
        [CustomAction]
        public static ActionResult InstallService(Session session)
        {
            return session.HandleErrors(() =>
            {
                ProcessExtensions.StartInstallUtilElevated(session.Property("StrongName"), true, "/AssemblyName");
            });
        }

        [CustomAction]
        public static ActionResult UnInstallService(Session session)
        {
            return session.HandleErrors(() =>
            {
                ProcessExtensions.StartInstallUtilElevated(session.Property("StrongName"), false, "/AssemblyName");
            });
        }

    }
}