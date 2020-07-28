pushd %~dp0
"C:\Program Files (x86)\Microsoft SDKs\Windows\v10.0A\bin\NETFX 4.8 Tools\x64\gacutil.exe" /i bin\release\InventoryWMIProvider.dll
C:\Windows\Microsoft.NET\Framework64\v4.0.30319\Installutil.exe bin\release\InventoryWMIProvider.dll
popd
