﻿namespace LauncherGUI
{
    partial class LauncherGUI
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LauncherGUI));
            this.label1 = new System.Windows.Forms.Label();
            this.txtLocal = new System.Windows.Forms.TextBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.btLocal = new System.Windows.Forms.Button();
            this.fdDBFinder = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Local da db:";
            // 
            // txtLocal
            // 
            this.txtLocal.Enabled = false;
            this.txtLocal.Location = new System.Drawing.Point(75, 7);
            this.txtLocal.Name = "txtLocal";
            this.txtLocal.Size = new System.Drawing.Size(311, 20);
            this.txtLocal.TabIndex = 1;
            // 
            // btIniciar
            // 
            this.btIniciar.Enabled = false;
            this.btIniciar.Location = new System.Drawing.Point(473, 4);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(75, 23);
            this.btIniciar.TabIndex = 2;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.BtIniciar_Click);
            // 
            // btLocal
            // 
            this.btLocal.Location = new System.Drawing.Point(392, 5);
            this.btLocal.Name = "btLocal";
            this.btLocal.Size = new System.Drawing.Size(75, 23);
            this.btLocal.TabIndex = 3;
            this.btLocal.Text = "Alterar Local";
            this.btLocal.UseVisualStyleBackColor = true;
            this.btLocal.Click += new System.EventHandler(this.BtLocal_Click);
            // 
            // fdDBFinder
            // 
            this.fdDBFinder.Filter = "Arquivos db|*.db";
            this.fdDBFinder.Title = "Selecione o local da db";
            this.fdDBFinder.FileOk += new System.ComponentModel.CancelEventHandler(this.FdDBFinder_FileOk);
            // 
            // LauncherGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(551, 32);
            this.Controls.Add(this.btLocal);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.txtLocal);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.Name = "LauncherGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayura Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LauncherGUI_FormClosed);
            this.Load += new System.EventHandler(this.LauncherGUI_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtLocal;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.Button btLocal;
        private System.Windows.Forms.OpenFileDialog fdDBFinder;
    }
}
