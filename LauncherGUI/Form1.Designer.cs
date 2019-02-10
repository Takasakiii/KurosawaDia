namespace LauncherGUI
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lbToken = new System.Windows.Forms.Label();
            this.txtToken = new System.Windows.Forms.TextBox();
            this.btIniciar = new System.Windows.Forms.Button();
            this.lbPrefix = new System.Windows.Forms.Label();
            this.txtPrefix = new System.Windows.Forms.TextBox();
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
            this.txtToken.Text = "NDU4NDYyNjgxMTk2NjU4Njg4.D0Cb9Q.RtCa7bjsi1RNLCZT3Mv2WLuFqvk";
            // 
            // btIniciar
            // 
            this.btIniciar.Location = new System.Drawing.Point(173, 69);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(67, 20);
            this.btIniciar.TabIndex = 2;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.btIniciar_Click);
            // 
            // lbPrefix
            // 
            this.lbPrefix.AutoSize = true;
            this.lbPrefix.Location = new System.Drawing.Point(3, 46);
            this.lbPrefix.Name = "lbPrefix";
            this.lbPrefix.Size = new System.Drawing.Size(45, 13);
            this.lbPrefix.TabIndex = 3;
            this.lbPrefix.Text = "Prefixo: ";
            // 
            // txtPrefix
            // 
            this.txtPrefix.Location = new System.Drawing.Point(43, 43);
            this.txtPrefix.Name = "txtPrefix";
            this.txtPrefix.Size = new System.Drawing.Size(378, 20);
            this.txtPrefix.TabIndex = 4;
            this.txtPrefix.Text = "\'";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 93);
            this.Controls.Add(this.txtPrefix);
            this.Controls.Add(this.lbPrefix);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.txtToken);
            this.Controls.Add(this.lbToken);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Teste iniciador do bot";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbToken;
        private System.Windows.Forms.TextBox txtToken;
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.Label lbPrefix;
        private System.Windows.Forms.TextBox txtPrefix;
    }
}

