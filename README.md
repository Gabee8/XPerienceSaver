# XPerienceSaver
[![Version](https://img.shields.io/badge/0.1-passing?label=Release)](https://github.com/Gabee8/XPerienceSaver/releases/tag/Release)
[![License](https://img.shields.io/github/license/Gabee8/XPerienceSaver)](https://github.com/Gabee8/XPerienceSaver/blob/main/LICENSE)

Simple Windows Xp style screensaver for Windows 7,8,10,11. Written C# by Visual Studio 2010

Installing:
- Unpack the scr file and right click install menu.
- or Unpack the scr file and copy C:\Windows\System32 folder.

System Requiments .net framework 4.0 or newer.

-------------
The program configure the screensaver settings menu:
- Select 4 logo's (XP, XP Media Center, Vista, Windows 7)
- Set custom logo
- Set animation time

Settings edit by registry:

```
[HKEY_CURRENT_USER\Software\XP_ScreenSaver]
```
Reg string's:
imagePath = custom path the logo

logo = selected logo (0 = XP, 1 = XP MCE, 2 = Vista, 3= Win7, 4 = Custom logo)

time = animation time (second)

Thanks idea and basic code for **Frank McCown**

https://sites.harding.edu/fmccown/screensaver/screensaver.html

Screenshots:

![](http://tandemradio.hu/wp-content/uploads/Kepernyokep-2025-07-26-214935.png)

![xpscr](https://github.com/user-attachments/assets/329a2eae-56b7-41c8-98e2-3e11cfcc4ad0)
