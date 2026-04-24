using System;
using System.Drawing;
using System.Windows.Forms;

namespace Reminder
{
    public class TrayManager
    {
        private static TrayManager _instance;
        private NotifyIcon _trayIcon;

        public static TrayManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TrayManager();
                return _instance;
            }
        }

        private TrayManager() { }

        public void Initialize(NotifyIcon icon)
        {
            _trayIcon = icon;
        }

        public void UpdateTooltip(string text)
        {
            if (_trayIcon != null)
                _trayIcon.Text = text.Length > 63 ? text.Substring(0, 63) : text;
        }

        public void UpdateIcon(Icon icon)
        {
            if (_trayIcon != null)
                _trayIcon.Icon = icon;
        }
    }
}
