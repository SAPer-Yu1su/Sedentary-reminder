# Sedentary Reminder ⏱️

<div align="center">

![License](https://img.shields.io/badge/license-MIT-blue.svg)
![Platform](https://img.shields.io/badge/platform-Windows-lightgrey.svg)
![.NET](https://img.shields.io/badge/.NET%20Framework-4.8-purple.svg)

**A warm Windows desktop app that guards your health with scientific work/rest cycles**

English | [简体中文](./README.md)

</div>

---

## ✨ Why Do You Need It?

> 💻 Prolonged sitting = Chronic suicide  
> 🏥 Cervical spondylosis, lumbar disease, eye strain... These occupational diseases are quietly approaching  
> ⏰ But we always "forget" to rest until our body sends warnings

**Sedentary Reminder** helps you develop healthy work habits in a gentle yet firm way.

---

## 🎯 Core Features

### 🔄 Smart Work/Rest Cycles
- Customizable work duration (1-120 minutes)
- Customizable rest duration (1-30 minutes)
- Automatic循环, no manual intervention needed
- Auto-start on boot (optional)

### 🎨 Exquisite Visual Experience
- **Main Window**: Modern card design with fade-in animation
- **Work Timer**: Rounded floating window with breathing color animation
- **Rest Screen**: Full-screen eye-care dark green with breathing ring animation
- **15-Second Warning**: Window changes color in the last 15 seconds

### 💬 Warm Copywriting Design
- **16 Encouragements**: Different warm greetings for each rest
- **16 Exercise Suggestions**: From serious to relaxed, there's always one for you
- **Time-Based Greetings**: Morning, lunch, evening... Different care at different times

### 🔒 Forced Rest Mode (Optional)
- Lock keyboard and mouse during rest
- Force you to leave the computer and truly rest
- Requires administrator privileges

### 🎈 Other Thoughtful Features
- System tray minimization
- Single instance to prevent duplicate launches
- Countdown number bounce animation
- Button breathing effect

---

## 📸 Interface Preview

### Main Interfaces

<table>
  <tr>
    <td align="center"><b>Main Configuration</b></td>
    <td align="center"><b>Work Timer</b></td>
  </tr>
  <tr>
    <td><img src="ScreenShot/久坐设置界面.png" width="350"/></td>
    <td><img src="ScreenShot/工作时长界面.png" width="350"/></td>
  </tr>
  <tr>
    <td align="center"><b>Rest Fullscreen</b></td>
    <td align="center"><b>Demo Animation</b></td>
  </tr>
  <tr>
    <td><img src="ScreenShot/休息界面.png" width="350"/></td>
    <td><img src="ScreenShot/演示动画.gif" width="350"/></td>
  </tr>
</table>

### Health Risks of Prolonged Sitting

<img src="ScreenShot/久坐危害报道.png" width="600"/>

---

## 🚀 Quick Start

### System Requirements

| Item | Requirement |
|------|-------------|
| OS | Windows 7 / 10 / 11 |
| .NET Framework | 4.8 |
| Admin Rights | Only required for input locking feature |

### Download & Install

1. Download the latest version from [Releases](https://github.com/SAPer-Yu1su/Sedentary-reminder/releases)
2. Extract to any directory
3. Double-click `Reminder.exe` to launch

**To use input locking feature**:
- Right-click `Reminder.exe` → **Run as administrator**
- Or right-click → **Properties** → **Compatibility** → Check **Run this program as an administrator**

---

## 📖 User Guide

### 1️⃣ Configure Work/Rest Time

<img src="ScreenShot/久坐设置界面.png" width="400"/>

- Set **Work Duration** (recommended 25-45 minutes)
- Set **Rest Duration** (recommended 5-10 minutes)
- Optional: Check **Lock keyboard and mouse during rest**
- Optional: Check **Auto-start on boot**
- Click **Start Working**

### 2️⃣ Work Phase

<img src="ScreenShot/工作时长界面.png" width="200"/>

- Floating window appears at bottom-right corner
- Draggable to any position
- Breathing color animation
- Last 15 seconds of countdown:
  - Window turns orange-red
  - Shows "⚠️ Time to rest!"

### 3️⃣ Rest Phase

<img src="ScreenShot/休息界面.png" width="400"/>

- Full-screen eye-care dark green background
- Random encouragements and exercise suggestions
- Time-based greetings
- Breathing ring animation
- Countdown number bounce effect
- If locking enabled: Keyboard and mouse completely disabled
- If locking disabled: Press `ESC` or `Alt+F4` to exit

### 4️⃣ System Tray

- Automatically minimizes to tray when main window closes
- Right-click tray icon:
  - **Preferences** — Open main configuration
  - **About** — View version info
  - **Exit** — Completely close the program

---

## 🎨 Design Philosophy

### Visual Design
- **Main Window**: Modern card design, gradient background, rounded shadow
- **Work Timer**: Emerald green gradient, rounded window, breathing animation
- **Rest Screen**: Eye-care dark green, breathing rings, particle effects

### Interaction Design
- **Fade-in on Start**: Window appears elegantly from transparent to opaque
- **Button Breathing**: Main button continuously breathes to attract clicks
- **Number Bounce**: Numbers enlarge and shrink when countdown changes
- **Time Awareness**: Different greetings based on time of day

### Copywriting Design
- **Warm Encouragement**: "🌻 The sun is just right, the breeze is gentle, perfect for daydreaming"
- **Relaxed Suggestions**: "🎵 Follow the rhythm: play a favorite song and sway freely"
- **Time Greetings**: "🌅 Good morning! A new day starts with health"

---

## 🛠️ Technical Architecture

### Project Structure

```
Sedentary-reminder/
├── Reminder/
│   ├── MainFrm.cs              # Main configuration window
│   ├── WorkFrm.cs              # Work timer
│   ├── RestFrm.cs              # Rest fullscreen
│   ├── KeyboardBlocker.cs      # Input locking
│   ├── AnimationController.cs  # Animation controller
│   ├── SessionManager.cs       # Session management
│   ├── ConfigManager.cs        # Configuration management
│   ├── TrayManager.cs          # Tray management
│   └── asset/                  # Icon resources
├── README.md                   # Chinese documentation
├── README_EN.md                # English documentation
└── .gitignore                  # Git ignore rules
```

### Tech Stack

- **Language**: C# (.NET Framework 4.8)
- **UI Framework**: Windows Forms
- **Graphics**: GDI+ (System.Drawing)
- **Animation**: Timer + Custom animation controller
- **Input Locking**: Win32 API `BlockInput`

### Core Flow

```
Program.cs (Single instance check)
    ↓
MainFrm (Configuration UI)
    ↓ Click "Start Working"
SessionManager.StartWorkSession()
    ↓
WorkFrm (Work countdown)
    ↓ Countdown ends
SessionManager.TransitionToRest()
    ↓
RestFrm (Rest screen)
    ↓ Countdown ends
SessionManager.TransitionToWork()
    ↓ Loop
WorkFrm ...
```

---

## ❓ FAQ

<details>
<summary><b>Q: Keyboard/mouse locking doesn't work?</b></summary>

A: Please ensure you **run as administrator**. The `BlockInput` API silently fails under normal privileges.
</details>

<details>
<summary><b>Q: How to emergency exit when locked?</b></summary>

A: Press `Ctrl+Alt+Del` simultaneously, then choose shutdown, log off, or switch user. This key combination cannot be blocked.
</details>

<details>
<summary><b>Q: Does the program save my settings?</b></summary>

A: Yes! Work duration, rest duration, and input locking options are automatically saved to `config.json`. Auto-start setting is saved in the registry.
</details>

<details>
<summary><b>Q: Can I pause the countdown?</b></summary>

A: The current version doesn't support pause. The design philosophy is "forced rest" to avoid procrastination.
</details>

<details>
<summary><b>Q: Why is the rest screen dark green?</b></summary>

A: Dark green is an eye-care color with low brightness, suitable for long viewing. This is a health feature, not an aesthetic choice.
</details>

---

## 🔨 Build from Source

### Prerequisites
- Visual Studio 2019 or higher
- .NET Framework 4.8 SDK

### Build Steps

```bash
# Clone repository
git clone https://github.com/SAPer-Yu1su/Sedentary-reminder.git
cd Sedentary-reminder

# Open with Visual Studio
start Reminder.sln

# Or use MSBuild command line
msbuild Reminder.sln /p:Configuration=Release /t:Rebuild
```

Build output is located at `Reminder/bin/Release/Reminder.exe`

---

## 🤝 Contributing

Contributions, bug reports, and suggestions are welcome!

### How to Contribute

1. Fork this repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Submit a Pull Request

### Contribution Ideas

- 🎨 **UI/UX Improvements**: More beautiful interface, smoother animations
- 🌍 **Multi-language Support**: Japanese, Korean, French...
- 🎵 **Sound Effects**: Audio reminders for rest start/end
- 📊 **Statistics**: Track work duration, rest count
- 🎮 **Rest Mini-games**: Interactive games during rest
- ⚙️ **More Configurations**: Theme switching, custom colors

---

## 📜 License

This project is licensed under the [MIT License](LICENSE).

---

## 💖 Acknowledgments

This project is based on [@wjbgis](https://github.com/wjbgis)'s [Sedentary-reminder](https://github.com/wjbgis/Sedentary-reminder) with optimizations and enhancements.

Thanks to the original author for the open-source contribution, helping more people focus on healthy work habits.

---

## 📮 Contact

- **GitHub Issues**: [Submit an issue](https://github.com/SAPer-Yu1su/Sedentary-reminder/issues)
- **GitHub Discussions**: [Join discussion](https://github.com/SAPer-Yu1su/Sedentary-reminder/discussions)

---

<div align="center">

**⭐ If this project helps you, please give it a Star!**

Made with ❤️ for your health

</div>
