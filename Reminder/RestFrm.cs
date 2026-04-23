using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Reminder
{
    public partial class RestFrm : Form
    {
        private int rst_m;
        private int wrk_m;
        private int rst_m2;
        private bool input_flag;
        private int rst_s = 0;
        private Timer fadeTimer;
        private Timer breatheTimer;
        private Timer animTimer;
        private double breatheOpacity = 0.88;
        private int arcAngle = 0;
        private int breathePhase = 0;
        private bool breatheIncreasing = true;
        private Button btnClose;

        private string[] encouragements = new string[]
        {
            "💪 站起来活动一下，身体会感谢你！",
            "🌟 休息一下，让眼睛放松放松~",
            "🎯 伸个懒腰，扭扭脖子，继续加油！",
            "☕ 喝口水，走动走动，健康最重要！",
            "🌈 短暂的休息，是为了更好的工作！",
            "🚀 动一动，让大脑更清醒！",
            "💚 保护颈椎，从现在开始！",
            "✨ 深呼吸，放松肩膀，你做得很棒！"
        };

        private string[] exercises = new string[]
        {
            "👀 眼保健操：远眺窗外，放松眼部肌肉",
            "🙆 颈部运动：左右转动，缓解颈部疲劳",
            "💪 肩部放松：耸肩、转肩，舒展肩膀",
            "🤸 腰部扭转：左右扭腰，活动腰椎",
            "🦵 腿部拉伸：站立抬腿，促进血液循环",
            "🧘 深呼吸：腹式呼吸，放松身心",
            "👐 手腕运动：转动手腕，预防鼠标手",
            "🚶 原地踏步：活动全身，提神醒脑"
        };

        public RestFrm() { InitializeComponent(); this.Paint += RestFrm_Paint; EnableDoubleBuffering(); }

        public RestFrm(int rst_minutes, int wrk_minutes, bool input_flag)
        {
            InitializeComponent(); this.Paint += RestFrm_Paint;
            EnableDoubleBuffering();
            this.rst_m = rst_minutes;
            this.wrk_m = wrk_minutes;
            this.rst_m2 = rst_minutes;
            this.input_flag = input_flag;
            Random rand = new Random();
            this.lblEncourage.Text = encouragements[rand.Next(encouragements.Length)];
            this.lblExercise.Text = exercises[rand.Next(exercises.Length)];
        }

        private void EnableDoubleBuffering()
        {
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.DoubleBuffered = true;
        }

        private void RestFrm_Load(object sender, EventArgs e)
        {
            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            int labelX = (sw - 900) / 2;

            // Re-position all labels to be horizontally centered
            this.lblTitle.Location = new Point(labelX, 80);
            this.lblTime.Location = new Point(labelX, 140);
            this.lblEncourage.Location = new Point(labelX, 300);
            this.lblExercise.Location = new Point(labelX, 360);
            this.lblHint.Location = new Point(labelX, sh - 80);

            InitCloseButton();
            lblHint.Text = input_flag
                ? "请休息，计时结束将自动继续"
                : "按 ESC 或 Alt+F4 退出休息";
            this.TopMost = true;
            this.WindowState = FormWindowState.Maximized;
            if (input_flag) KeyboardBlocker.off();
            UpdateTimeLabel();
            FadeIn();
            StartBreatheAnimation();
            StartAnimTimer();
            timerRst.Start();
        }

        private void InitCloseButton()
        {
            btnClose = new Button();
            btnClose.Size = new Size(36, 36);
            btnClose.Text = "✕";
            btnClose.Font = new Font("Microsoft YaHei UI", 14F, FontStyle.Bold);
            btnClose.ForeColor = Color.FromArgb(180, 200, 220);
            btnClose.BackColor = Color.FromArgb(30, 50, 80);
            btnClose.FlatStyle = FlatStyle.Flat;
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.Cursor = Cursors.Hand;
            int btnX = Screen.PrimaryScreen.Bounds.Width - 36 - 16;
            btnClose.Location = new Point(btnX, 16);
            btnClose.Click += (s, ex) => ExitRest();
            btnClose.MouseEnter += (s, ex) => { btnClose.BackColor = Color.FromArgb(80, 40, 60); btnClose.ForeColor = Color.White; };
            btnClose.MouseLeave += (s, ex) => { btnClose.BackColor = Color.FromArgb(30, 50, 80); btnClose.ForeColor = Color.FromArgb(180, 200, 220); };
            this.Controls.Add(btnClose);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape || keyData == (Keys.Alt | Keys.F4)) { ExitRest(); return true; }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void ExitRest()
        {
            StopAllTimers();
            if (input_flag) KeyboardBlocker.on();
            var workFrm = new WorkFrm(wrk_m, rst_m2, input_flag);
            this.Hide();
            workFrm.Show();
        }

        private void UpdateTimeLabel() { lblTime.Text = rst_m.ToString("D2") + ":" + rst_s.ToString("D2"); }

        private void FadeIn()
        {
            if (fadeTimer != null) return;
            this.Opacity = 0;
            fadeTimer = new Timer { Interval = 40 };
            fadeTimer.Tick += (s, e) => { if (this.Opacity < 0.88) this.Opacity += 0.04; else { fadeTimer.Stop(); fadeTimer.Dispose(); fadeTimer = null; } };
            fadeTimer.Start();
        }

        private void StartBreatheAnimation()
        {
            if (breatheTimer != null) return;
            breatheOpacity = 0.88;
            breatheTimer = new Timer { Interval = 300 };
            breatheTimer.Tick += (s, e) =>
            {
                breatheOpacity += breatheIncreasing ? 0.005 : -0.005;
                if (breatheOpacity >= 0.92) breatheIncreasing = false;
                else if (breatheOpacity <= 0.88) breatheIncreasing = true;
                this.Opacity = breatheOpacity;
            };
            breatheTimer.Start();
        }

        private void StartAnimTimer()
        {
            if (animTimer != null) return;
            animTimer = new Timer { Interval = 200 };
            animTimer.Tick += (s, e) =>
            {
                arcAngle = (arcAngle + 2) % 360;
                breathePhase = (breathePhase + 1) % 100;
                this.Invalidate();
            };
            animTimer.Start();
        }

        private void TimerRst_Tick(object sender, EventArgs e) { timing(); }

        private void timing()
        {
            if (rst_s > 0)
            {
                rst_s--;
                UpdateTimeLabel();
            }
            else
            {
                timerRst.Enabled = false;
                rst_m--;
                if (rst_m >= 0)
                {
                    rst_s = 59;
                    UpdateTimeLabel();
                    timerRst.Enabled = true;
                }
                else
                {
                    ExitRest();
                    return;
                }
            }
        }

        private void StopAllTimers()
        {
            if (breatheTimer != null) { breatheTimer.Stop(); breatheTimer.Dispose(); breatheTimer = null; }
            if (fadeTimer != null) { fadeTimer.Stop(); fadeTimer.Dispose(); fadeTimer = null; }
            if (animTimer != null) { animTimer.Stop(); animTimer.Dispose(); animTimer = null; }
        }

        private void RestFrm_FormClosed(object sender, FormClosedEventArgs e) { }
        private void RestFrm_FormClosing(object sender, FormClosingEventArgs e) { StopAllTimers(); }

        private void RestFrm_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            int sw = Screen.PrimaryScreen.Bounds.Width;
            int sh = Screen.PrimaryScreen.Bounds.Height;
            int cx = sw / 2;
            int cy = sh / 2;

            using (var bgBrush = new LinearGradientBrush(
                new Rectangle(0, 0, sw, sh),
                Color.FromArgb(8, 22, 18),
                Color.FromArgb(5, 14, 12),
                LinearGradientMode.Vertical))
            {
                g.FillRectangle(bgBrush, 0, 0, sw, sh);
            }

            float breathe = (float)Math.Sin(breathePhase * Math.PI / 50.0);

            int[] baseRadii = { 260, 200, 140 };
            int[][] ringAlphas = {
                new int[] { 30, 55 },
                new int[] { 20, 40 },
                new int[] { 15, 30 }
            };
            Color[][] ringColors = {
                new Color[] { Color.FromArgb(50, 160, 120), Color.FromArgb(25, 120, 90) },
                new Color[] { Color.FromArgb(40, 130, 100), Color.FromArgb(20, 90, 70) },
                new Color[] { Color.FromArgb(30, 100, 80), Color.FromArgb(15, 70, 55) }
            };

            for (int i = 0; i < 3; i++)
            {
                int r = baseRadii[i] + (int)(breathe * 8);
                int alpha = ringAlphas[i][0] + (int)(breathe * (ringAlphas[i][1] - ringAlphas[i][0]));
                Color c = Color.FromArgb(alpha, ringColors[i][0].R, ringColors[i][0].G, ringColors[i][0].B);
                using (var pen = new Pen(c, 1.5f))
                {
                    g.DrawEllipse(pen, cx - r, cy - r, r * 2, r * 2);
                }
            }

            int arcR = 290 + (int)(breathe * 6);
            using (var arcPen = new Pen(Color.FromArgb(160, 220, 180), 3) { StartCap = LineCap.Round, EndCap = LineCap.Round })
            {
                g.DrawArc(arcPen, cx - arcR, cy - arcR, arcR * 2, arcR * 2, arcAngle - 90, 60);
            }
        }
    }
}
