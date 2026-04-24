using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;
using Microsoft.Win32;

namespace Reminder
{
    public partial class MainFrm : Form
    {
        private Timer _startAnimTimer;
        private Timer _buttonPulseTimer;
        private double _pulsePhase = 0;

        public MainFrm()
        {
            InitializeComponent();
            SetupTrayEvents();
            this.Load += MainFrm_Load;
            this.Opacity = 0;
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            TrayManager.Instance.Initialize(notifyIcon1);
            TrayManager.Instance.UpdateTooltip("久坐提醒");

            var config = ConfigManager.LoadConfig();
            numWrkTime.Value = config.settings.workMinutes;
            numRstTime.Value = config.settings.restMinutes;
            ckBoxInput.Checked = config.settings.inputBlockingEnabled;

            ckBoxAutoStart.Checked = IsAutoStartEnabled();
            ckBoxAutoStart.CheckedChanged += CkBoxAutoStart_CheckedChanged;

            StartFadeInAnimation();
            StartButtonPulseAnimation();
        }

        private void StartFadeInAnimation()
        {
            _startAnimTimer = new Timer { Interval = 30 };
            _startAnimTimer.Tick += (s, e) =>
            {
                if (this.Opacity < 1.0)
                    this.Opacity += 0.08;
                else
                {
                    _startAnimTimer.Stop();
                    _startAnimTimer.Dispose();
                    _startAnimTimer = null;
                }
            };
            _startAnimTimer.Start();
        }

        private void StartButtonPulseAnimation()
        {
            _buttonPulseTimer = new Timer { Interval = 50 };
            _buttonPulseTimer.Tick += (s, e) =>
            {
                _pulsePhase = (_pulsePhase + 0.05) % (Math.PI * 2);
                double pulse = Math.Sin(_pulsePhase) * 0.5 + 0.5;
                int colorShift = (int)(pulse * 15);
                btnStart.BackColor = Color.FromArgb(0, 145 + colorShift, 130 + colorShift);
            };
            _buttonPulseTimer.Start();
        }

        private void CkBoxAutoStart_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (ckBoxAutoStart.Checked)
                {
                    SetAutoStart(true);
                }
                else
                {
                    SetAutoStart(false);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"设置开机自启动失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ckBoxAutoStart.Checked = !ckBoxAutoStart.Checked;
            }
        }

        private bool IsAutoStartEnabled()
        {
            try
            {
                using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", false))
                {
                    if (key != null)
                    {
                        object value = key.GetValue("SedentaryReminder");
                        return value != null;
                    }
                }
            }
            catch { }
            return false;
        }

        private void SetAutoStart(bool enable)
        {
            string appPath = Application.ExecutablePath;
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true))
            {
                if (key != null)
                {
                    if (enable)
                    {
                        key.SetValue("SedentaryReminder", appPath);
                    }
                    else
                    {
                        key.DeleteValue("SedentaryReminder", false);
                    }
                }
            }
        }

        private void SetupTrayEvents()
        {
            // Double-click to show main window
            notifyIcon1.DoubleClick += (s, e) => { this.Visible = true; this.WindowState = FormWindowState.Normal; this.ShowInTaskbar = true; };

            主窗体ToolStripMenuItem.Click += (s, e) =>
            {
                this.Visible = true;
                this.WindowState = FormWindowState.Normal;
                this.ShowInTaskbar = true;
                SessionManager.Instance.StopSession();
            };
            
            // About with version
            string version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            关于ToolStripMenuItem.Text = "关于 v" + version;
            关于ToolStripMenuItem.Click += (s, e) => { var ab = new AboutBox(); ab.ShowDialog(); };
            
            exit_ToolStripMenuItem.Click += (s, e) => { notifyIcon1.Visible = false; System.Environment.Exit(0); };

            btnStart.MouseEnter += (s, e) => { btnStart.BackColor = Color.FromArgb(0, 125, 112); };
            btnStart.MouseLeave += (s, e) => { btnStart.BackColor = Color.FromArgb(0, 145, 130); };
            btnStart.MouseDown += (s, e) => { btnStart.BackColor = Color.FromArgb(0, 115, 102); };
            btnStart.MouseUp += (s, e) => { btnStart.BackColor = Color.FromArgb(0, 125, 112); };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(240, 242, 245),
                Color.FromArgb(230, 233, 238),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            }

            Rectangle shadowBounds = new Rectangle(
                this.pnlCard.Bounds.X + 2,
                this.pnlCard.Bounds.Y + 2,
                this.pnlCard.Bounds.Width,
                this.pnlCard.Bounds.Height
            );
            int radius = 16;

            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddArc(shadowBounds.X, shadowBounds.Y, radius * 2, radius * 2, 180, 90);
                shadowPath.AddArc(shadowBounds.Right - radius * 2, shadowBounds.Y, radius * 2, radius * 2, 270, 90);
                shadowPath.AddArc(shadowBounds.Right - radius * 2, shadowBounds.Bottom - radius * 2, radius * 2, radius * 2, 0, 90);
                shadowPath.AddArc(shadowBounds.X, shadowBounds.Bottom - radius * 2, radius * 2, radius * 2, 90, 90);
                shadowPath.CloseFigure();

                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(25, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }

            base.OnPaint(e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ConfigManager.UpdateSettings(
                (int)numWrkTime.Value,
                (int)numRstTime.Value,
                ckBoxInput.Checked
            );

            var config = new SessionConfig
            {
                WorkMinutes = (int)numWrkTime.Value,
                RestMinutes = (int)numRstTime.Value,
                InputBlockingEnabled = ckBoxInput.Checked
            };

            SessionManager.Instance.StartWorkSession(config);
            this.Visible = false;
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_startAnimTimer != null)
            {
                _startAnimTimer.Stop();
                _startAnimTimer.Dispose();
                _startAnimTimer = null;
            }
            if (_buttonPulseTimer != null)
            {
                _buttonPulseTimer.Stop();
                _buttonPulseTimer.Dispose();
                _buttonPulseTimer = null;
            }

            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
        }

    }
}
