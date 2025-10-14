using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Configuration.Install;

namespace UsbCopyMon.SetupActions
{
    [RunInstaller(true)]
    public sealed class CleanupActions : System.Configuration.Install.Installer
    {
        public override void Uninstall(IDictionary savedState)
        {
            Run("taskkill.exe", "/F /T /IM UsbCopyMon.Tray.exe");
            Run("sc.exe", "stop UsbCopyMonService");
            Run("sc.exe", "delete UsbCopyMonService");
            Run("schtasks.exe", "/End /TN \"UsbCopyMon Tray\"");
            Run("schtasks.exe", "/Delete /TN \"UsbCopyMon Tray\" /F");
            base.Uninstall(savedState);
        }

        static void Run(string tool, string args)
        {
            var sys = System.Environment.SystemDirectory;     // C:\Windows\System32
            var path = Path.Combine(sys, tool);
            if (!File.Exists(path)) path = tool;

            try
            {
                var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                p.Start();
                p.WaitForExit(8000);
            }
            catch { /* swallow on uninstall */ }
        }
    }
}
