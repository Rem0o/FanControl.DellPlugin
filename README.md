# FanControl.DellPlugin [![Build status](https://ci.appveyor.com/api/projects/status/aqevcsrb976mavdo/branch/master?svg=true)](https://ci.appveyor.com/project/Rem0o/fancontrol-dellplugin/branch/master)

Plugin for [FanControl](https://github.com/Rem0o/FanControl.Releases) that provides support for Dell laptops using [DellFanManagement](https://github.com/AaronKelley/DellFanManagement).

## To install

Either
* Download the latest binaries from [AppVeyor](https://ci.appveyor.com/project/Rem0o/fancontrol-dellplugin/branch/master/artifacts)
* Compile the solution.

and copy FanControl.DellPlugin.dll into FanControl's "plugins" folder.
You might need to "unlock" the dll in its properties.

## Special note

Setting this registry key might be required on your machine, see <br>
* http://forum.notebookreview.com/threads/dellfanmanagement-dellfankeepalive-%E2%80%93-tools-for-managing-the-fan-speed-in-dell-laptops.833340/ 
* https://www.geoffchappell.com/notes/security/whqlsettings/index.htm

```
[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\CI\Policy]
"UpgradedSystem"=dword:00000000
```

