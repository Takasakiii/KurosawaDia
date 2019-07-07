namespace Bot.Forms
{
    partial class LogForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.txLog = new System.Windows.Forms.TextBox();
            this.btLimpar = new System.Windows.Forms.Button();
            this.btDesligar = new System.Windows.Forms.Button();
            this.controlNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.sairToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aaaToolStripMenuItem = new System.Windows.Forms.ToolStripSeparator();
            this.verLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.desligarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // txLog
            // 
            this.txLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txLog.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txLog.Location = new System.Drawing.Point(0, 0);
            this.txLog.Multiline = true;
            this.txLog.Name = "txLog";
            this.txLog.ReadOnly = true;
            this.txLog.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txLog.Size = new System.Drawing.Size(798, 448);
            this.txLog.TabIndex = 0;
            // 
            // btLimpar
            // 
            this.btLimpar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btLimpar.Location = new System.Drawing.Point(642, 425);
            this.btLimpar.Name = "btLimpar";
            this.btLimpar.Size = new System.Drawing.Size(75, 23);
            this.btLimpar.TabIndex = 1;
            this.btLimpar.TabStop = false;
            this.btLimpar.Text = "Limpar";
            this.btLimpar.UseVisualStyleBackColor = true;
            this.btLimpar.Click += new System.EventHandler(this.BtLimpar_Click);
            // 
            // btDesligar
            // 
            this.btDesligar.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btDesligar.Location = new System.Drawing.Point(723, 425);
            this.btDesligar.Name = "btDesligar";
            this.btDesligar.Size = new System.Drawing.Size(75, 23);
            this.btDesligar.TabIndex = 2;
            this.btDesligar.TabStop = false;
            this.btDesligar.Text = "Desligar";
            this.btDesligar.UseVisualStyleBackColor = true;
            this.btDesligar.Click += new System.EventHandler(this.BtDesligar_Click);
            // 
            // controlNotify
            // 
            this.controlNotify.ContextMenuStrip = this.contextMenuStrip;
            this.controlNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("controlNotify.Icon")));
            this.controlNotify.Text = "Ayura";
            this.controlNotify.Visible = true;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sairToolStripMenuItem,
            this.aaaToolStripMenuItem,
            this.verLogsToolStripMenuItem,
            this.desligarToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(119, 76);
            // 
            // sairToolStripMenuItem
            // 
            this.sairToolStripMenuItem.Name = "sairToolStripMenuItem";
            this.sairToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.sairToolStripMenuItem.Text = "Sair";
            this.sairToolStripMenuItem.Click += new System.EventHandler(this.SairToolStripMenuItem_Click);
            // 
            // aaaToolStripMenuItem
            // 
            this.aaaToolStripMenuItem.Name = "aaaToolStripMenuItem";
            this.aaaToolStripMenuItem.Size = new System.Drawing.Size(115, 6);
            // 
            // verLogsToolStripMenuItem
            // 
            this.verLogsToolStripMenuItem.Name = "verLogsToolStripMenuItem";
            this.verLogsToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.verLogsToolStripMenuItem.Text = "Ver Logs";
            this.verLogsToolStripMenuItem.Click += new System.EventHandler(this.VerLogsToolStripMenuItem_Click);
            // 
            // desligarToolStripMenuItem
            // 
            this.desligarToolStripMenuItem.Name = "desligarToolStripMenuItem";
            this.desligarToolStripMenuItem.Size = new System.Drawing.Size(118, 22);
            this.desligarToolStripMenuItem.Text = "Desligar";
            this.desligarToolStripMenuItem.Click += new System.EventHandler(this.BtDesligar_Click);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btDesligar);
            this.Controls.Add(this.btLimpar);
            this.Controls.Add(this.txLog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "LogForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Log";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            this.Resize += new System.EventHandler(this.LogForm_Resize);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txLog;
        private System.Windows.Forms.Button btLimpar;
        private System.Windows.Forms.Button btDesligar;
        private System.Windows.Forms.NotifyIcon controlNotify;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem desligarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem verLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem sairToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator aaaToolStripMenuItem;
    }
}