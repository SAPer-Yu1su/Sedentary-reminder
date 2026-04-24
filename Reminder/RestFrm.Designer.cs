namespace Reminder
{
    partial class RestFrm
    {
        private System.ComponentModel.IContainer components = null;
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerRst = new System.Windows.Forms.Timer(this.components);
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblTime = new System.Windows.Forms.Label();
            this.lblEncourage = new System.Windows.Forms.Label();
            this.lblExercise = new System.Windows.Forms.Label();
            this.lblHint = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // timerRst
            //
            this.timerRst.Interval = 1000;
            this.timerRst.Tick += new System.EventHandler(this.TimerRst_Tick);
            //
            // lblTitle
            //
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 26F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(180, 230, 210);
            this.lblTitle.Location = new System.Drawing.Point(0, 80);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(900, 50);
            this.lblTitle.TabIndex = 5;
            this.lblTitle.Text = "☕ 休息一下";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTime
            //
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 80F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(0, 140);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(900, 130);
            this.lblTime.TabIndex = 0;
            this.lblTime.Text = "00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblEncourage
            //
            this.lblEncourage.BackColor = System.Drawing.Color.Transparent;
            this.lblEncourage.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular);
            this.lblEncourage.ForeColor = System.Drawing.Color.FromArgb(200, 230, 220);
            this.lblEncourage.Location = new System.Drawing.Point(0, 300);
            this.lblEncourage.Name = "lblEncourage";
            this.lblEncourage.Size = new System.Drawing.Size(900, 50);
            this.lblEncourage.TabIndex = 3;
            this.lblEncourage.Text = "";
            this.lblEncourage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblExercise
            //
            this.lblExercise.BackColor = System.Drawing.Color.Transparent;
            this.lblExercise.Font = new System.Drawing.Font("Microsoft YaHei UI", 14F, System.Drawing.FontStyle.Regular);
            this.lblExercise.ForeColor = System.Drawing.Color.FromArgb(140, 190, 170);
            this.lblExercise.Location = new System.Drawing.Point(0, 360);
            this.lblExercise.Name = "lblExercise";
            this.lblExercise.Size = new System.Drawing.Size(900, 40);
            this.lblExercise.TabIndex = 4;
            this.lblExercise.Text = "";
            this.lblExercise.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblHint
            //
            this.lblHint.BackColor = System.Drawing.Color.Transparent;
            this.lblHint.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F, System.Drawing.FontStyle.Regular);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(100, 190, 170);
            this.lblHint.Location = new System.Drawing.Point(0, 560);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(900, 26);
            this.lblHint.TabIndex = 12;
            this.lblHint.Text = "";
            this.lblHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // RestFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(8, 22, 18);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblEncourage);
            this.Controls.Add(this.lblExercise);
            this.Controls.Add(this.lblHint);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RestFrm";
            this.ShowInTaskbar = false;
            this.Text = "RestFrm";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RestFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.RestFrm_FormClosed);
            this.Load += new System.EventHandler(this.RestFrm_Load);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerRst;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblEncourage;
        private System.Windows.Forms.Label lblExercise;
        private System.Windows.Forms.Label lblHint;
    }
}
