using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Reflection;

namespace Reminder
{
    public partial class MainFrm : Form
    {
        WorkFrm wrkFrm;
        private Timer trayUpdateTimer;

        public MainFrm()
        {
            InitializeComponent();
            SetupTrayEvents();
            InitTrayUpdate();
        }

        private void InitTrayUpdate()
        {
            trayUpdateTimer = new Timer { Interval = 1000 };
            trayUpdateTimer.Tick += (s, e) => UpdateTrayTooltip();
            trayUpdateTimer.Start();
        }

        public static void UpdateTrayTooltip(string state, int minutes, int seconds)
        {
            if (state == "work")
            {
                string timeStr = minutes.ToString("D2") + ":" + seconds.ToString("D2");
                // Use the main form's NotifyIcon if it exists
                var mainFrm = Application.OpenForms["MainFrm"] as MainFrm;
                if (mainFrm != null && mainFrm.notifyIcon1 != null)
                {
                    mainFrm.notifyIcon1.Text = "工作中 · 剩余 " + timeStr;
                }
            }
            else if (state == "rest")
            {
                var mainFrm = Application.OpenForms["MainFrm"] as MainFrm;
                if (mainFrm != null && mainFrm.notifyIcon1 != null)
                {
                    mainFrm.notifyIcon1.Text = "休息时间到！";
                }
            }
        }

        private void UpdateTrayTooltip()
        {
            // Called by timer when no work is running
            var mainFrm = Application.OpenForms["MainFrm"] as MainFrm;
            if (mainFrm != null && mainFrm.notifyIcon1 != null && wrkFrm == null)
            {
                mainFrm.notifyIcon1.Text = "久坐提醒";
            }
        }

        private void SetupTrayEvents()
        {
            // Double-click to show main window
            notifyIcon1.DoubleClick += (s, e) => { this.Visible = true; this.WindowState = FormWindowState.Normal; this.ShowInTaskbar = true; };

            主窗体ToolStripMenuItem.Click += (s, e) => { this.Visible = true; this.WindowState = FormWindowState.Normal; this.ShowInTaskbar = true; if (wrkFrm != null) wrkFrm.Close(); };
            
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
            // Draw subtle gradient background
            using (LinearGradientBrush gradientBrush = new LinearGradientBrush(
                this.ClientRectangle,
                Color.FromArgb(237, 239, 242),
                Color.FromArgb(227, 231, 236),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(gradientBrush, this.ClientRectangle);
            }

            // Draw shadow for pnlCard: offset (+3,+3), alpha=30, corner radius=20
            Rectangle shadowBounds = new Rectangle(
                this.pnlCard.Bounds.X + 3,
                this.pnlCard.Bounds.Y + 3,
                this.pnlCard.Bounds.Width,
                this.pnlCard.Bounds.Height
            );
            int radius = 20;

            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                // Top-left corner
                shadowPath.AddArc(shadowBounds.X - radius, shadowBounds.Y - radius, radius * 2, radius * 2, 180, -90);
                // Top edge
                shadowPath.AddLine(shadowBounds.X + radius, shadowBounds.Y, shadowBounds.Right - radius, shadowBounds.Y);
                // Top-right corner
                shadowPath.AddArc(shadowBounds.Right - radius, shadowBounds.Y - radius, radius * 2, radius * 2, 270, -90);
                // Right edge
                shadowPath.AddLine(shadowBounds.Right, shadowBounds.Y + radius, shadowBounds.Right, shadowBounds.Bottom - radius);
                // Bottom-right corner
                shadowPath.AddArc(shadowBounds.Right - radius, shadowBounds.Bottom - radius, radius * 2, radius * 2, 0, -90);
                // Bottom edge
                shadowPath.AddLine(shadowBounds.Right - radius, shadowBounds.Bottom, shadowBounds.X + radius, shadowBounds.Bottom);
                // Bottom-left corner
                shadowPath.AddArc(shadowBounds.X - radius, shadowBounds.Bottom - radius, radius * 2, radius * 2, 90, -90);
                // Left edge
                shadowPath.AddLine(shadowBounds.X, shadowBounds.Bottom - radius, shadowBounds.X, shadowBounds.Y + radius);
                shadowPath.CloseFigure();

                using (SolidBrush shadowBrush = new SolidBrush(Color.FromArgb(30, 0, 0, 0)))
                {
                    e.Graphics.FillPath(shadowBrush, shadowPath);
                }
            }

            base.OnPaint(e);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            wrkFrm = new WorkFrm((int)numWrkTime.Value, (int)numRstTime.Value, ckBoxInput.Checked);
            wrkFrm.Show();
            this.Visible = false;
        }

        private void MainFrm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.WindowState = FormWindowState.Minimized;
            this.Visible = false;
            this.ShowInTaskbar = false;
        }
    }
}
