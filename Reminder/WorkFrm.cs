using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Reminder
{
    public partial class WorkFrm : Form
    {
        private int wrk_minutes;
        private int wrk_seconds;
        private int wrk_m;
        private int rst_minutes;
        private bool input_flag;
        private bool left_flag;
        private Point mouseoff;
        private Timer fadeTimer;
        private Timer breatheAnimTimer;
        private double targetOpacity = 0.92;
        private bool isWarning;
        private int breathePhase = 0;

        public WorkFrm()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            this.Paint += WorkFrm_Paint;
            SetupPosition();
            SetupRegion();
            this.Load += Form1_Load;
        }

        public WorkFrm(int wrk_minutes, int rst_minutes, bool input_flag)
        {
            InitializeComponent();
            EnableDoubleBuffering();
            this.Paint += WorkFrm_Paint;
            this.wrk_minutes = wrk_minutes;
            this.rst_minutes = rst_minutes;
            this.wrk_m = wrk_minutes;
            this.input_flag = input_flag;
            SetupPosition();
            SetupRegion();
            FadeIn();
            this.Load += Form1_Load;
        }

        private void EnableDoubleBuffering()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }

        private void SetupPosition()
        {
            int x = Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width - 16;
            int y = Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height - 16;
            this.Location = new Point(x, y);
        }

        private void SetupRegion()
        {
            using (GraphicsPath path = CreateRoundedRectPath(this.ClientRectangle, 18))
            {
                this.Region = new Region(path);
            }
        }

        private GraphicsPath CreateRoundedRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            path.AddArc(rect.Right - radius, rect.Y, radius, radius, 270, 90);
            path.AddArc(rect.Right - radius, rect.Bottom - radius, radius, radius, 0, 90);
            path.AddArc(rect.X, rect.Bottom - radius, radius, radius, 90, 90);
            path.CloseFigure();
            return path;
        }

        private void WorkFrm_Paint(object sender, PaintEventArgs e)
        {
            if (DesignMode) return;
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            float breathe = (float)Math.Sin(breathePhase * Math.PI / 50.0);

            Color color1, color2;
            if (isWarning)
            {
                color1 = Color.FromArgb((int)(180 + breathe * 50), (int)(90 + breathe * 35), (int)(20 + breathe * 15));
                color2 = Color.FromArgb((int)(140 + breathe * 40), (int)(60 + breathe * 25), (int)(10 + breathe * 10));
            }
            else
            {
                color1 = Color.FromArgb(0, 95, 80);
                color2 = Color.FromArgb(0, 65, 55);
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, color1, color2, LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            DrawShadow(g);

            if (isWarning)
            {
                int borderAlpha = (int)(120 + breathe * 80);
                using (Pen outerPen = new Pen(Color.FromArgb(borderAlpha, 255, 140, 0), 2))
                {
                    g.DrawRectangle(outerPen, 0, 0, this.Width - 1, this.Height - 1);
                }
                int innerAlpha = (int)(60 + breathe * 40);
                using (Pen innerPen = new Pen(Color.FromArgb(innerAlpha, 255, 180, 50), 1))
                {
                    g.DrawRectangle(innerPen, 2, 2, this.Width - 5, this.Height - 5);
                }
            }
            else
            {
                using (Pen borderPen = new Pen(Color.FromArgb(60, 0, 80, 68), 1))
                {
                    g.DrawRectangle(borderPen, 0, 0, this.Width - 1, this.Height - 1);
                }
            }
        }

        private void DrawShadow(Graphics g)
        {
            int offset = 4;
            int shadowAlpha = 40;
            using (GraphicsPath path = CreateRoundedRectPath(this.ClientRectangle, 18))
            {
                using (Pen shadowPen = new Pen(Color.FromArgb(shadowAlpha, 0, 0, 0), offset * 2))
                {
                    shadowPen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    Matrix transform = new Matrix();
                    transform.Translate(offset, offset);
                    g.Transform = transform;
                    g.DrawPath(shadowPen, path);
                    g.Transform = new Matrix();
                }
            }
        }

        private void FadeIn()
        {
            if (fadeTimer != null) return;
            this.Opacity = 0;
            fadeTimer = new Timer { Interval = 30 };
            fadeTimer.Tick += (s, e) =>
            {
                if (this.Opacity < targetOpacity)
                    this.Opacity += 0.06;
                else { fadeTimer.Stop(); fadeTimer.Dispose(); fadeTimer = null; }
            };
            fadeTimer.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            wrk_seconds = 0;
            UpdateTimeLabel();
            lblTitle.Text = "\x2318 工作时间";
        }

        private void UpdateTimeLabel()
        {
            lblTime.Text = wrk_minutes.ToString("D2") + ":" + wrk_seconds.ToString("D2");
            MainFrm.UpdateTrayTooltip("work", wrk_minutes, wrk_seconds);
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timing();
        }

        private void timing()
        {
            Warn();

            if (wrk_seconds > 0)
            {
                wrk_seconds--;
                UpdateTimeLabel();
            }
            else
            {
                timerWrk.Enabled = false;
                wrk_minutes--;
                if (wrk_minutes >= 0)
                {
                    wrk_seconds = 59;
                    UpdateTimeLabel();
                    timerWrk.Enabled = true;
                }
                else
                {
                    this.Close();
                    MainFrm.UpdateTrayTooltip("rest", 0, 0);
                    RestFrm restFrm = new RestFrm(rst_minutes, wrk_m, input_flag);
                    restFrm.ShowDialog();
                    return;
                }
            }
        }

        private void Warn()
        {
            if (wrk_minutes == 0 && wrk_seconds <= 16)
            {
                if (!isWarning)
                {
                    isWarning = true;
                    StartPulseAnimation();
                }
                float breathe = (float)Math.Sin(breathePhase * Math.PI / 50.0);
                int green = (int)(180 + breathe * 75);
                lblWarn.ForeColor = Color.FromArgb(255, green, 0);
                lblWarn.Visible = true;
                lblWarn.Text = "\x26a0\xfe0f 该休息了！";
            }
            else if (isWarning && !(wrk_minutes == 0 && wrk_seconds <= 16))
            {
                isWarning = false;
                StopPulseAnimation();
                lblWarn.Visible = false;
                lblWarn.ForeColor = Color.FromArgb(255, 210, 80);
                SetupPosition();
            }
        }

        private void StartPulseAnimation()
        {
            if (breatheAnimTimer != null) return;
            breatheAnimTimer = new Timer { Interval = 80 };
            breatheAnimTimer.Tick += (s, e) =>
            {
                breathePhase = (breathePhase + 1) % 100;
                this.Invalidate();
            };
            breatheAnimTimer.Start();
        }

        private void StopPulseAnimation()
        {
            if (breatheAnimTimer != null)
            {
                breatheAnimTimer.Stop();
                breatheAnimTimer.Dispose();
                breatheAnimTimer = null;
            }
            breathePhase = 0;
        }

        private void CenterOnScreen()
        {
            int x = (Screen.PrimaryScreen.WorkingArea.Size.Width - this.Width) / 2;
            int y = (Screen.PrimaryScreen.WorkingArea.Size.Height - this.Height) / 2;
            this.Location = new Point(x, y);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                const int WS_EX_APPWINDOW = 0x40000;
                const int WS_EX_TOOLWINDOW = 0x80;
                CreateParams cp = base.CreateParams;
                cp.ExStyle &= (~WS_EX_APPWINDOW);
                cp.ExStyle |= WS_EX_TOOLWINDOW;
                return cp;
            }
        }

        private void WorkFrm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseoff = new Point(e.X, e.Y);
                left_flag = true;
            }
        }

        private void WorkFrm_MouseMove(object sender, MouseEventArgs e)
        {
            if (left_flag)
            {
                Point mouseSet = Control.MousePosition;
                mouseSet.Offset(-mouseoff.X, -mouseoff.Y);
                Location = mouseSet;
            }
        }

        private void WorkFrm_MouseUp(object sender, MouseEventArgs e)
        {
            left_flag = false;
        }
    }
}
