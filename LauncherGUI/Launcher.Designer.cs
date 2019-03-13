﻿namespace LauncherGUI
{
    partial class launcherGUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(launcherGUI));
            this.lbToken = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.lbPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
            this.lbWeebToken = new System.Windows.Forms.Label();
            this.txtWeeb = new System.Windows.Forms.TextBox();
            this.btDesligar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbToken
            // 
            this.lbToken.AutoSize = true;
            this.lbToken.Location = new System.Drawing.Point(3, 10);
            this.lbToken.Name = "lbToken";
            this.lbToken.Size = new System.Drawing.Size(44, 13);
            this.lbToken.TabIndex = 0;
            this.lbToken.Text = "Token: ";
            // 
            // txtToken
            // 
            this.txtToken.Location = new System.Drawing.Point(43, 7);
            this.txtToken.Name = "txtToken";
            this.txtToken.Size = new System.Drawing.Size(378, 20);
            this.txtToken.TabIndex = 1;
            this.txtToken.Text = "NTI3MjM2OTYxODcwODA3MDQx.D0ZuBA.HdHvsnLvlOspsPBj2Ueko6i6BjA";
            // 
            // btIniciar
            // 
            this.btIniciar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btIniciar.Location = new System.Drawing.Point(127, 96);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(67, 23);
            this.btIniciar.TabIndex = 2;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // lbPrefix
            // 
            this.lbPrefix.AutoSize = true;
            this.lbPrefix.Location = new System.Drawing.Point(2, 42);
            this.lbPrefix.Name = "lbPrefix";
            this.lbPrefix.Size = new System.Drawing.Size(45, 13);
            this.lbPrefix.TabIndex = 3;
            this.lbPrefix.Text = "Prefixo: ";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(43, 39);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(378, 20);
            this.txtPrefix.TabIndex = 4;
            this.txtPrefix.Text = "\'";
            // 
            // lbWeebToken
            // 
            this.lbWeebToken.AutoSize = true;
            this.lbWeebToken.Location = new System.Drawing.Point(2, 73);
            this.lbWeebToken.Name = "lbWeebToken";
            this.lbWeebToken.Size = new System.Drawing.Size(39, 13);
            this.lbWeebToken.TabIndex = 6;
            this.lbWeebToken.Text = "Weeb:";
            // 
            // txtWeeb
            // 
            this.txtWeeb.Location = new System.Drawing.Point(43, 70);
            this.txtWeeb.Name = "txtWeeb";
            this.txtWeeb.Size = new System.Drawing.Size(378, 20);
            this.txtWeeb.TabIndex = 7;
            this.txtWeeb.Text = "SEpGb1JpYkJtOjUzMGQ0MWE4YTkwZDNiOGU0NWFkZDhjOGQzODBmMDhmZDVjNDQ4ZmM0OWQ3YjdhNzI5Z" +
    "mU2NWJj";
            // 
            // btDesligar
            // 
            this.btDesligar.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btDesligar.Enabled = false;
            this.btDesligar.Location = new System.Drawing.Point(210, 96);
            this.btDesligar.Name = "btDesligar";
            this.btDesligar.Size = new System.Drawing.Size(75, 23);
            this.btDesligar.TabIndex = 8;
            this.btDesligar.Text = "Desligar";
            this.btDesligar.UseVisualStyleBackColor = true;
            this.btDesligar.Click += new System.EventHandler(this.btDesligar_Click);
            // 
            // launcherGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(427, 122);
            this.Controls.Add(this.btDesligar);
            this.Controls.Add(this.txtWeeb);
            this.Controls.Add(this.lbWeebToken);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.lbPrefix);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.lbToken);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.Name = "launcherGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayura Launcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbToken;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.Label lbPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
        private System.Windows.Forms.Label lbWeebToken;
        private System.Windows.Forms.TextBox txtWeeb;
        private System.Windows.Forms.Button btDesligar;
    }
}
