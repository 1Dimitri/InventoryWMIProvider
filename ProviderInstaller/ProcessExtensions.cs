using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Text;
using WixSharp.CommonTasks;

namespace ProviderInstaller
{

    public static class ProcessExtensions
    {
        // This is taken for a yet not released version of WixSharp, see https://github.com/oleg-shilo/wixsharp/issues/892
        public static Process StartElevated(this string fileName, string args = "")
        {
            bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

            return Process.Start(new ProcessStartInfo
            {
                WorkingDirectory = Path.GetDirectoryName(Path.GetFullPath(fileName)),
                FileName = fileName,
                Arguments = args,
                UseShellExecute = true,
                Verb = isAdmin ? "" : "runas"
            });
        }

        public static Process StartInstallUtilElevated(string assemblyFilePathOrName, bool isInstalling, string args = null)
        {
            string InstallUtilExePath = System.IO.Path.Combine(Tasks.CurrentFrameworkDirectory, "InstallUtil.exe");
            string InstallUtilArguments = string.Format("{0} {1} \"{2}\"", isInstalling ? "" : "/u", args ?? "", assemblyFilePathOrName);
            return StartElevated(InstallUtilExePath, InstallUtilArguments);
        }
    }
}
