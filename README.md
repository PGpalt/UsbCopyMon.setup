Windows Installer for the UsbCopyMon Service.

Clone The UsbCopyMon.setup Repo first and then UsbCopyMonitor in the same Parent Directory.
Open UsbCopyMon.sln in the UsbCopyMonitor directory in Visual Studio and 
1. Publish the UsbCopyMon.service as a folder in the location: bin\Release\net8.0-windows\win-x64\publish\service-publish
Go to Show all settings and set the following:
   Target Framework: net8.0-windows
   Deployment mode: Self-contained.
   Target runtime: win-x64
   Click save and Publish.
2. Publish the UsbCopyMon.tray as a folder in the location: bin\Release\net8.0-windows\win-x64\publish\tray-publish
Go to Show all settings and set the following:
   Target Framework: net8.0-windows
   Deployment mode: Self-contained.
   Target runtime: win-x64
   Click save and Publish.
3. Build UsbCopyMon.Shared.
4. Open the UsbCopyMon.setup.sln in the UsbCopyMon.setup directory and build UsbCopyMon.SetupActions.
5. Go back to UsbCopyMon.sln and build UsbCopyMon.setup.
6. Right click UsbCopyMon.setup and Open Folder in File Explorer.
7. The .msi installer is located in the debug folder.
8. Can install the .msi unattended in a cmd terminal with admin rights by running msiexec /i "UsbCopyMon.setup.msi" /qn /norestart /L*v "%TEMP%\UsbCopyMonInstall.log"
9. Can uninstall with msiexec /x "UsbCopyMon.setup.msi" /qn /norestart /L*v "%TEMP%\UsbCopyMonUninstall.log"
