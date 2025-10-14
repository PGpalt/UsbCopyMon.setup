using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace UsbCopyMon.SetupActions
{
    [RunInstaller(true)]
    public sealed class CleanupActions : System.Configuration.Install.Installer
    {
        public override void Uninstall(IDictionary savedState)
        {
            RunTool("taskkill.exe", "/F /T /IM UsbCopyMon.Tray.exe");
            RunTool("sc.exe", "stop UsbCopyMonService");
            RunTool("sc.exe", "delete UsbCopyMonService");
            RunTool("schtasks.exe", "/End /TN \"UsbCopyMon Tray\"");
            RunTool("schtasks.exe", "/Delete /TN \"UsbCopyMon Tray\" /F");
            base.Uninstall(savedState);
        }

        static void RunTool(string tool, string args)
        {
            var full = Path.Combine(System.Environment.SystemDirectory, tool);
            if (!File.Exists(full)) full = tool; // fallback in case

            try
            {
                using var p = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = full,
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        WindowStyle = ProcessWindowStyle.Hidden
                    }
                };
                p.Start();
                p.WaitForExit(8000);
            }
            catch { /* ignore on uninstall */ }
        }
    }
}
