# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**Sedentary Reminder (久坐提醒小工具)** is a Windows desktop application that enforces timed work/rest intervals to combat prolonged sedentary behavior. Users configure work duration and rest duration, then the app cycles between a floating work timer and a mandatory rest overlay. When input blocking is enabled, the rest period physically prevents keyboard/mouse use to force genuine breaks.

**Target Platform:** Windows 7/10/11
**Framework:** .NET Framework 4.8
**UI Framework:** WinForms
**Languages:** UI text in Chinese; logic and variable names in English

## Project Structure

```
Sedentary-reminder/
├── Reminder.sln                          # Visual Studio solution file
├── Reminder/
│   ├── Sedentary Reminder.csproj         # Project file (.NET 4.8)
│   ├── Program.cs                        # Entry point, single-instance enforcement
│   ├── app.config                        # .NET runtime configuration
│   ├── KeyboardBlocker.cs                # Win32 BlockInput API wrapper
│   ├── MainFrm.cs / .Designer.cs / .resx     # Main config window + system tray
│   ├── WorkFrm.cs / .Designer.cs / .resx     # Floating work countdown timer
│   ├── RestFrm.cs / .Designer.cs / .resx     # Full-screen rest overlay
│   ├── AboutBox.cs / .Designer.cs / .resx   # About dialog with GitHub link
│   ├── Properties/
│   │   ├── AssemblyInfo.cs               # Assembly metadata (v1.0.0.0)
│   │   ├── Resources.resx / .Designer.cs # Bitmap image resources (icons)
│   │   └── Settings.settings / .Designer.cs  # User settings (currently unused)
│   └── asset/
│       ├── sit_32.png / sit_64.png / sit_128.png   # Work period icons
│       ├── sport_64_01~03.png            # Exercise/sport icons
│       ├── ICO.ico / ICO2.ico / sit_128.ico  # Application icons
│       └── time.ico                      # Timer icon
└── ScreenShot/                           # README screenshot images
```

## Building and Running

```bash
# Build (requires Visual Studio or MSBuild Tools)
msbuild Reminder.sln /p:Configuration=Debug
msbuild Reminder.sln /p:Configuration=Release

# Run
./Reminder/bin/Debug/Reminder.exe         # Debug build
./Reminder/bin/Release/Reminder.exe        # Release build
```

**Note:** The keyboard/mouse blocking feature requires running as Administrator (uses `BlockInput` Win32 API).

## Application State Machine

The app cycles through three forms in a loop:

```
┌─────────────┐   Start    ┌─────────────┐  Work timer  ┌─────────────┐
│   MainFrm   │ ─────────► │   WorkFrm    │ ──────────► │   RestFrm   │
│  (Config)   │             │ (Floating    │              │  (Full      │
│             │ ◄───────── │   Timer)     │  ◄─────────  │  Screen)    │
└─────────────┘  Stop/Exit │             │  Rest done   │             │
                            └─────────────┘              └─────────────┘
```

### Form Details

**MainFrm** (主窗体)
- Entry point and configuration panel
- NumericUpDown controls for work minutes (1–120) and rest minutes (1–30)
- Checkbox to enable/disable input blocking during rest
- Start button launches WorkFrm and hides MainFrm
- Minimizes to system tray on close (not taskbar)
- Tray icon context menu: "Show main window" / "Exit"
- Exposes `public` fields: `wrkTime` (int), `rstTime` (int), `input_flag` (bool) passed to WorkFrm constructor

**WorkFrm** (工作窗体)
- Floating, draggable, always-on-top countdown timer
- Position: bottom-right of primary screen (x = Screen.Width - 180, y = Screen.Height - 100)
- Initial opacity: 0.9 with fade-in animation (increments of 0.05 every 30ms)
- Teal gradient background (Color.FromArgb 0,150,136 → 0,121,107)
- Rounded rectangle region (corner radius 20)
- Countdown format: `MM:SS` with leading zeros
- **15-second warning:** At `wrk_minutes==0 && wrk_seconds<=16`, background turns tomato red (255,99,71), window centers on screen, yellow warning label appears: "⚠️ 该休息了！"
- Hides from Alt+Tab via `WS_EX_TOOLWINDOW` extended style
- On timer expiry: closes self, launches RestFrm modally

**RestFrm** (休息窗体)
- Full-screen maximized overlay (0.75–0.85 opacity with breathing animation)
- Dark blue-gray gradient background (Color.FromArgb 44,62,80 → 52,73,94)
- Decorative concentric circle outlines centered on screen
- Displays random encouragement message + random exercise suggestion from hardcoded arrays (8 items each)
- If input blocking disabled: shows "Alt+F4 退出本界面" hint
- On load: calls `KeyboardBlocker.off()` to lock input (if enabled)
- On timer expiry: calls `KeyboardBlocker.on()` to unlock, then launches new WorkFrm and closes self

## Key Components

### KeyboardBlocker.cs
```csharp
// Win32 API
[DllImport("user32.dll")]
static extern void BlockInput(bool Block);

// Public methods
public static bool off()   // Blocks keyboard+mouse (calls BlockInput(true))
public static bool on()    // Unblocks keyboard+mouse (calls BlockInput(false))
public static bool IsAdministrator()  // Checks WindowsBuiltInRole.Administrator
```
- `off()` and `on()` return false silently if not admin (no exception thrown)
- BlockInput does NOT intercept Ctrl+Alt+Del (handled at kernel level)
- Blocking cannot be bypassed through Task Manager — Task Manager itself is blocked

### Program.cs — Single Instance Enforcement
```csharp
// Iterates all running processes by name; if match found (different PID), shows
// MessageBox "程序运行中，见右下角系统托盘" and exits with code 0 (no error)
```
- Uses `Process.GetProcesses()` (not `Process.GetCurrentProcess()`) to find duplicates

### Timer Mechanism
- Each form uses `System.Windows.Forms.Timer` with 1-second interval
- Countdown logic uses recursive method calls: `timing()` calls itself after re-enabling the timer
- No threading, no async — all UI-bound on the main WinForms message loop

### AboutBox.cs
- Displays assembly metadata (Title, Version, Description)
- "linkLabel1" opens GitHub repo URL via `System.Diagnostics.Process.Start()`

## Configuration and State

**No persistent settings.** All configuration is session-only:
- Work/rest durations entered on MainFrm are passed via constructor arguments
- Input blocking preference passed as `bool input_flag`
- `Properties\Settings.settings` is empty (no user settings defined)
- `app.config` only declares supported .NET 4.8 runtime

## Win32 API Details

| API | DLL | Purpose |
|-----|-----|---------|
| `BlockInput(bool)` | user32.dll | Enable/disable all keyboard and mouse input |

The `IsAdministrator()` check uses `WindowsIdentity.GetCurrent()` + `WindowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator)`.

## UI Constants and Magic Numbers

| Location | Constant | Value | Meaning |
|----------|----------|-------|---------|
| WorkFrm | `targetOpacity` | 0.9 | WorkFrm target opacity |
| WorkFrm | `fadeTimer.Interval` | 30ms | Opacity fade step interval |
| WorkFrm | opacity increment | 0.05 | Each fade step adds 5% opacity |
| WorkFrm | warning trigger | `wrk_minutes==0 && wrk_seconds<=16` | 16-second warning window |
| WorkFrm | corner radius | 20 | Rounded rectangle radius |
| RestFrm | `breatheOpacity` range | 0.75–0.85 | Breathing animation opacity range |
| RestFrm | `breatheTimer.Interval` | 50ms | Breathing animation step interval |
| RestFrm | opacity increment | 0.01 | Each breathe step changes opacity by 1% |
| RestFrm | fade-in target | 0.85 | RestFrm fade-in target opacity |
| RestFrm | `fadeTimer.Interval` | 30ms | Same fade mechanism as WorkFrm |

## Form Window Styles

**WorkFrm** uses `WS_EX_TOOLWINDOW` (0x80) to hide from Alt+Tab, and clears `WS_EX_APPWINDOW` (0x40000) to hide from taskbar.

**RestFrm** uses default window styles but sets `TopMost = true` and `WindowState = Maximized`.

## Development Notes

- **Language:** Chinese for all UI labels and messages; English for variable/method names and comments
- **Thread safety:** None — all code runs on the main UI thread
- **Error handling:** Minimal — no try/catch blocks, no logging framework
- **Resource management:** Icons and PNGs stored as project resources (not loaded from disk at runtime)
- **No NuGet packages** — only built-in .NET Framework assemblies

## Known Limitations

- **No persistent settings** — durations and input-blocking preference reset on each launch
- **No pause/resume** — timers cannot be paused once started
- **Admin required for blocking** — without elevation, input blocking silently fails (returns false)
- **Single monitor only** — uses `Screen.PrimaryScreen` for positioning
- **No multi-language support** — all UI text is hardcoded Chinese
- **No accessibility features** — screen reader support not implemented
- **No DPI awareness** — fixed pixel positions may look incorrect on high-DPI displays

# Agent Team PUA 配置
所有 teammate 开工前必须加载 pua skill。
teammate 失败 2 次以上时向 Leader 发送 [PUA-REPORT] 格式汇报。
Leader 负责全局压力等级管理和跨 teammate 失败传递。