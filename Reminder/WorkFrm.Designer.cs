namespace Reminder
{
    partial class WorkFrm
    {
        private System.ComponentModel.IContainer components = null;

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timerWrk = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblWarn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            //
            // timerWrk
            //
            this.timerWrk.Enabled = true;
            this.timerWrk.Interval = 1000;
            this.timerWrk.Tick += new System.EventHandler(this.Timer1_Tick);
            //
            // lblTitle
            //
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(200, 230, 220);
            this.lblTitle.Location = new System.Drawing.Point(0, 4);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(160, 16);
            this.lblTitle.TabIndex = 8;
            this.lblTitle.Text = "\u23F1  工作时间";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblTime
            //
            this.lblTime.BackColor = System.Drawing.Color.Transparent;
            this.lblTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblTime.ForeColor = System.Drawing.Color.White;
            this.lblTime.Location = new System.Drawing.Point(0, 22);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(160, 38);
            this.lblTime.TabIndex = 4;
            this.lblTime.Text = "00:00";
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblWarn
            //
            this.lblWarn.BackColor = System.Drawing.Color.Transparent;
            this.lblWarn.Font = new System.Drawing.Font("Microsoft YaHei UI", 7F, System.Drawing.FontStyle.Bold);
            this.lblWarn.ForeColor = System.Drawing.Color.FromArgb(255, 210, 80);
            this.lblWarn.Location = new System.Drawing.Point(0, 62);
            this.lblWarn.Name = "lblWarn";
            this.lblWarn.Size = new System.Drawing.Size(160, 14);
            this.lblWarn.TabIndex = 7;
            this.lblWarn.Text = "";
            this.lblWarn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblWarn.Visible = false;
            //
            // WorkFrm
            //
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(160, 80);
            this.Controls.Add(this.lblWarn);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.lblTitle);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.Name = "WorkFrm";
            this.Opacity = 0D;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "WorkFrm";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.WorkFrm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.WorkFrm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.WorkFrm_MouseUp);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timerWrk;
        private System.Windows.Forms.Label lblTime;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblWarn;
    }
}
