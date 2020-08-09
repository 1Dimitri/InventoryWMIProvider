These files are explaining what you would have to do to manually install the WMI Provider without the MSI Installer

# Installation
1. Install the DLL in the GAC (note that you must sign the assembly), using SDK's gacutil for example
2. Run the .NET XXXXinstaller classes (that's what does installutil)

#Removal
Use the reverse order with uninstall flag

