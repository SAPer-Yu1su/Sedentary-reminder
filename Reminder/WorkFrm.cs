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
        private AnimationController _animController;
        private double targetOpacity = 0.85;
        private bool isWarning;
        private bool sessionEnded = false;

        public WorkFrm()
        {
            InitializeComponent();
            EnableDoubleBuffering();
            this.Paint += WorkFrm_Paint;
            SetupPosition();
            SetupRegion();
            this.Load += Form1_Load;
        }

        public WorkFrm(SessionConfig config)
        {
            InitializeComponent();
            EnableDoubleBuffering();
            this.Paint += WorkFrm_Paint;
            this.wrk_minutes = config.WorkMinutes;
            this.rst_minutes = config.RestMinutes;
            this.wrk_m = config.WorkMinutes;
            this.input_flag = config.InputBlockingEnabled;

            _animController = new AnimationController(this);
            _animController.AnimationTick += (s, e) => this.Invalidate();

            SetupPosition();
            SetupRegion();
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

            float breathe = (float)Math.Sin(_animController.BreathePhase * Math.PI / 50.0);

            Color color1, color2;
            if (isWarning)
            {
                // 警告状态：橙红色呼吸渐变
                color1 = Color.FromArgb((int)(200 + breathe * 55), (int)(80 + breathe * 40), (int)(30 + breathe * 20));
                color2 = Color.FromArgb((int)(160 + breathe * 50), (int)(50 + breathe * 30), (int)(15 + breathe * 10));
            }
            else
            {
                // 正常状态：翡翠绿渐变，更明亮优雅
                color1 = Color.FromArgb((int)(0 + breathe * 20), (int)(155 + breathe * 15), (int)(140 + breathe * 10));
                color2 = Color.FromArgb((int)(0 + breathe * 15), (int)(120 + breathe * 12), (int)(105 + breathe * 8));
            }

            // 绘制主背景
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle, color1, color2, LinearGradientMode.ForwardDiagonal))
            {
                g.FillRectangle(brush, this.ClientRectangle);
            }

            // 顶部高光条，营造玻璃质感
            using (LinearGradientBrush highlightBrush = new LinearGradientBrush(
                new Rectangle(0, 0, this.Width, 8),
                Color.FromArgb(80, Color.White),
                Color.FromArgb(0, Color.White),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(highlightBrush, new Rectangle(0, 0, this.Width, 8));
            }

            // 警告时绘制柔和边框
            if (isWarning)
            {
                int borderAlpha = (int)(100 + breathe * 100);
                using (Pen borderPen = new Pen(Color.FromArgb(borderAlpha, 255, 180, 60), 2))
                {
                    using (GraphicsPath path = CreateRoundedRectPath(new Rectangle(0, 0, this.Width - 1, this.Height - 1), 18))
                    {
                        g.DrawPath(borderPen, path);
                    }
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

        private void Form1_Load(object sender, EventArgs e)
        {
            wrk_seconds = 0;
            UpdateTimeLabel();
            lblTitle.Text = "\u23F1  工作时间";
            _animController.StartFadeIn(targetOpacity);
        }

        private void UpdateTimeLabel()
        {
            lblTime.Text = wrk_minutes.ToString("D2") + ":" + wrk_seconds.ToString("D2");
            TrayManager.Instance.UpdateTooltip($"工作中 · 剩余 {lblTime.Text}");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timing();
        }

        private void timing()
        {
            if (sessionEnded) return;
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
                    sessionEnded = true;
                    timerWrk.Enabled = false;
                    SessionManager.Instance.TransitionToRest();
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
                    _animController.StartPulse();
                }
                float breathe = (float)Math.Sin(_animController.BreathePhase * Math.PI / 50.0);
                int green = (int)(180 + breathe * 75);
                lblWarn.ForeColor = Color.FromArgb(255, green, 0);
                lblWarn.Visible = true;
                lblWarn.Text = "⚠️ 该休息了！";
            }
            else if (isWarning && !(wrk_minutes == 0 && wrk_seconds <= 16))
            {
                isWarning = false;
                _animController.StopPulse();
                lblWarn.Visible = false;
                lblWarn.ForeColor = Color.FromArgb(255, 210, 80);
                SetupPosition();
            }
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _animController?.Dispose();
                components?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
