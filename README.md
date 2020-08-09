# InventoryWMIProvider

InventoryWMIProvider is a C#-based WMI Provider using the WMI Extensions for .NET Framework 3.5,

Primary goal is to have a set of classes for my computer inventory needs.
The classes are achieving different goals:
- adding information which is not available by WMI
- or available in a way which has side effects (e.g. Win32_Product)
- or available using somewhat complex queries and I prefer to use simpler ones in the client of those classes

It tries to put each WMI class into different .cs files so a modular WMI Provider can be built if needed.

## Installer
After reviewing and testing several options, creating a MSI file wasfound to be the easiest way to go.
I made several attempts at WiX and others, and WixSharp was  eventually the best tool to achieve this, thanks to Oleg Shilo for his [WixSharp](https://github.com/oleg-shilo/wixsharp) utility and his support.



