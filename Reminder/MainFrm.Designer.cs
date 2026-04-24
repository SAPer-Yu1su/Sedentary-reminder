namespace Reminder
{
    partial class MainFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.主窗体ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exit_ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlCard = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.pnlDivider = new System.Windows.Forms.Panel();
            this.lblWorkTime = new System.Windows.Forms.Label();
            this.numWrkTime = new Reminder.StyledNumericUpDown();
            this.lblWorkUnit = new System.Windows.Forms.Label();
            this.lblRestTime = new System.Windows.Forms.Label();
            this.numRstTime = new Reminder.StyledNumericUpDown();
            this.lblRestUnit = new System.Windows.Forms.Label();
            this.pnlDivider2 = new System.Windows.Forms.Panel();
            this.ckBoxInput = new System.Windows.Forms.CheckBox();
            this.lblInputHint = new System.Windows.Forms.Label();
            this.ckBoxAutoStart = new System.Windows.Forms.CheckBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.lblFooter = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            this.pnlCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).BeginInit();
            this.SuspendLayout();
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Sedentary Reminder";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(28, 28);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.主窗体ToolStripMenuItem,
            this.关于ToolStripMenuItem,
            this.exit_ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.contextMenuStrip1.Size = new System.Drawing.Size(148, 106);
            // 
            // 主窗体ToolStripMenuItem
            // 
            this.主窗体ToolStripMenuItem.Name = "主窗体ToolStripMenuItem";
            this.主窗体ToolStripMenuItem.Size = new System.Drawing.Size(147, 34);
            this.主窗体ToolStripMenuItem.Text = "首选项";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(147, 34);
            this.关于ToolStripMenuItem.Text = "关于";
            // 
            // exit_ToolStripMenuItem
            // 
            this.exit_ToolStripMenuItem.Name = "exit_ToolStripMenuItem";
            this.exit_ToolStripMenuItem.Size = new System.Drawing.Size(147, 34);
            this.exit_ToolStripMenuItem.Text = "退出";
            // 
            // pnlCard
            // 
            this.pnlCard.BackColor = System.Drawing.Color.White;
            this.pnlCard.Controls.Add(this.lblTitle);
            this.pnlCard.Controls.Add(this.lblSubtitle);
            this.pnlCard.Controls.Add(this.pnlDivider);
            this.pnlCard.Controls.Add(this.lblWorkTime);
            this.pnlCard.Controls.Add(this.numWrkTime);
            this.pnlCard.Controls.Add(this.lblWorkUnit);
            this.pnlCard.Controls.Add(this.lblRestTime);
            this.pnlCard.Controls.Add(this.numRstTime);
            this.pnlCard.Controls.Add(this.lblRestUnit);
            this.pnlCard.Controls.Add(this.pnlDivider2);
            this.pnlCard.Controls.Add(this.ckBoxInput);
            this.pnlCard.Controls.Add(this.lblInputHint);
            this.pnlCard.Controls.Add(this.ckBoxAutoStart);
            this.pnlCard.Controls.Add(this.btnStart);
            this.pnlCard.Controls.Add(this.lblFooter);
            this.pnlCard.Location = new System.Drawing.Point(24, 24);
            this.pnlCard.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlCard.Name = "pnlCard";
            this.pnlCard.Size = new System.Drawing.Size(520, 660);
            this.pnlCard.TabIndex = 1;
            //
            // lblTitle
            //
            this.lblTitle.Font = new System.Drawing.Font("Microsoft YaHei UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(130)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 30);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(520, 70);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "⏱  久坐提醒";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // lblSubtitle
            //
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 7F, System.Drawing.FontStyle.Italic);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(160)))), ((int)(((byte)(170)))));
            this.lblSubtitle.Location = new System.Drawing.Point(0, 100);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(520, 25);
            this.lblSubtitle.TabIndex = 2;
            this.lblSubtitle.Text = "S E D E N T A R Y   R E M I N D E R";
            this.lblSubtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // pnlDivider
            //
            this.pnlDivider.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.pnlDivider.Location = new System.Drawing.Point(40, 140);
            this.pnlDivider.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlDivider.Name = "pnlDivider";
            this.pnlDivider.Size = new System.Drawing.Size(440, 2);
            this.pnlDivider.TabIndex = 3;
            //
            // lblWorkTime
            //
            this.lblWorkTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.lblWorkTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblWorkTime.Location = new System.Drawing.Point(60, 200);
            this.lblWorkTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblWorkTime.Name = "lblWorkTime";
            this.lblWorkTime.Size = new System.Drawing.Size(140, 42);
            this.lblWorkTime.TabIndex = 3;
            this.lblWorkTime.Text = "工作时长";
            this.lblWorkTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // numWrkTime
            //
            this.numWrkTime.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.numWrkTime.Location = new System.Drawing.Point(210, 200);
            this.numWrkTime.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numWrkTime.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.numWrkTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numWrkTime.Name = "numWrkTime";
            this.numWrkTime.Size = new System.Drawing.Size(120, 42);
            this.numWrkTime.TabIndex = 4;
            this.numWrkTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numWrkTime.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            //
            // lblWorkUnit
            //
            this.lblWorkUnit.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblWorkUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblWorkUnit.Location = new System.Drawing.Point(340, 200);
            this.lblWorkUnit.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblWorkUnit.Name = "lblWorkUnit";
            this.lblWorkUnit.Size = new System.Drawing.Size(60, 42);
            this.lblWorkUnit.TabIndex = 5;
            this.lblWorkUnit.Text = "分钟";
            this.lblWorkUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // lblRestTime
            //
            this.lblRestTime.Font = new System.Drawing.Font("Microsoft YaHei UI", 11F);
            this.lblRestTime.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.lblRestTime.Location = new System.Drawing.Point(60, 280);
            this.lblRestTime.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRestTime.Name = "lblRestTime";
            this.lblRestTime.Size = new System.Drawing.Size(140, 42);
            this.lblRestTime.TabIndex = 6;
            this.lblRestTime.Text = "休息时长";
            this.lblRestTime.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            //
            // numRstTime
            //
            this.numRstTime.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.numRstTime.Location = new System.Drawing.Point(210, 280);
            this.numRstTime.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.numRstTime.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numRstTime.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numRstTime.Name = "numRstTime";
            this.numRstTime.Size = new System.Drawing.Size(120, 42);
            this.numRstTime.TabIndex = 7;
            this.numRstTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numRstTime.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            //
            // lblRestUnit
            //
            this.lblRestUnit.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.lblRestUnit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(110)))), ((int)(((byte)(120)))));
            this.lblRestUnit.Location = new System.Drawing.Point(340, 280);
            this.lblRestUnit.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblRestUnit.Name = "lblRestUnit";
            this.lblRestUnit.Size = new System.Drawing.Size(60, 42);
            this.lblRestUnit.TabIndex = 8;
            this.lblRestUnit.Text = "分钟";
            this.lblRestUnit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            //
            // pnlDivider2
            //
            this.pnlDivider2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.pnlDivider2.Location = new System.Drawing.Point(40, 360);
            this.pnlDivider2.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlDivider2.Name = "pnlDivider2";
            this.pnlDivider2.Size = new System.Drawing.Size(440, 2);
            this.pnlDivider2.TabIndex = 9;
            //
            // ckBoxInput
            //
            this.ckBoxInput.AutoSize = true;
            this.ckBoxInput.Checked = true;
            this.ckBoxInput.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckBoxInput.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ckBoxInput.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.ckBoxInput.Location = new System.Drawing.Point(110, 390);
            this.ckBoxInput.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ckBoxInput.Name = "ckBoxInput";
            this.ckBoxInput.Size = new System.Drawing.Size(270, 32);
            this.ckBoxInput.TabIndex = 9;
            this.ckBoxInput.Text = "休息时锁定键盘和鼠标";
            this.ckBoxInput.UseVisualStyleBackColor = true;
            //
            // lblInputHint
            //
            this.lblInputHint.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lblInputHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(160)))),
            ((int)(((byte)(170)))));
            this.lblInputHint.Location = new System.Drawing.Point(0, 430);
            this.lblInputHint.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblInputHint.Name = "lblInputHint";
            this.lblInputHint.Size = new System.Drawing.Size(520, 28);
            this.lblInputHint.TabIndex = 10;
            this.lblInputHint.Text = "（需以管理员权限运行）";
            this.lblInputHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // ckBoxAutoStart
            //
            this.ckBoxAutoStart.AutoSize = true;
            this.ckBoxAutoStart.Font = new System.Drawing.Font("Microsoft YaHei UI", 10F);
            this.ckBoxAutoStart.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(60)))), ((int)(((byte)(70)))));
            this.ckBoxAutoStart.Location = new System.Drawing.Point(110, 470);
            this.ckBoxAutoStart.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.ckBoxAutoStart.Name = "ckBoxAutoStart";
            this.ckBoxAutoStart.Size = new System.Drawing.Size(160, 32);
            this.ckBoxAutoStart.TabIndex = 13;
            this.ckBoxAutoStart.Text = "开机自动启动";
            this.ckBoxAutoStart.UseVisualStyleBackColor = true;
            //
            // btnStart
            //
            this.btnStart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(145)))), ((int)(((byte)(130)))));
            this.btnStart.FlatAppearance.BorderSize = 0;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Font = new System.Drawing.Font("Microsoft YaHei UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnStart.ForeColor = System.Drawing.Color.White;
            this.btnStart.Location = new System.Drawing.Point(70, 520);
            this.btnStart.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(380, 75);
            this.btnStart.TabIndex = 11;
            this.btnStart.Text = "开始工作";
            this.btnStart.UseVisualStyleBackColor = false;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            //
            // lblFooter
            //
            this.lblFooter.Font = new System.Drawing.Font("Microsoft YaHei UI", 8F);
            this.lblFooter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(160)))), ((int)(((byte)(170)))), ((int)(((byte)(180)))));
            this.lblFooter.Location = new System.Drawing.Point(0, 610);
            this.lblFooter.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
            this.lblFooter.Name = "lblFooter";
            this.lblFooter.Size = new System.Drawing.Size(520, 30);
            this.lblFooter.TabIndex = 12;
            this.lblFooter.Text = "健康工作 · 快乐生活";
            this.lblFooter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            //
            // MainFrm
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(239)))), ((int)(((byte)(242)))));
            this.ClientSize = new System.Drawing.Size(568, 708);
            this.Controls.Add(this.pnlCard);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainFrm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "久坐提醒";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.pnlCard.ResumeLayout(false);
            this.pnlCard.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numWrkTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numRstTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 主窗体ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exit_ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        private System.Windows.Forms.Panel pnlCard;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.Panel pnlDivider;
        private System.Windows.Forms.Label lblWorkTime;
        private StyledNumericUpDown numWrkTime;
        private System.Windows.Forms.Label lblWorkUnit;
        private System.Windows.Forms.Label lblRestTime;
        private StyledNumericUpDown numRstTime;
        private System.Windows.Forms.Label lblRestUnit;
        private System.Windows.Forms.Panel pnlDivider2;
        private System.Windows.Forms.CheckBox ckBoxInput;
        private System.Windows.Forms.Label lblInputHint;
        private System.Windows.Forms.CheckBox ckBoxAutoStart;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.Label lblFooter;
    }
}
