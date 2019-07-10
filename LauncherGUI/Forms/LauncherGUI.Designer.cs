namespace LauncherGUI
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
            this.btIniciar = new System.Windows.Forms.Button();
            this.fdDBFinder = new System.Windows.Forms.OpenFileDialog();
            this.btConfiguracoes = new System.Windows.Forms.Button();
            this.wbCustomizacao = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // btIniciar
            // 
            this.btIniciar.Enabled = false;
            this.btIniciar.Location = new System.Drawing.Point(349, 254);
            this.btIniciar.Name = "btIniciar";
            this.btIniciar.Size = new System.Drawing.Size(75, 23);
            this.btIniciar.TabIndex = 2;
            this.btIniciar.Text = "Iniciar";
            this.btIniciar.UseVisualStyleBackColor = true;
            this.btIniciar.Click += new System.EventHandler(this.BtIniciar_Click);
            // 
            // fdDBFinder
            // 
            this.fdDBFinder.Filter = "Arquivos db|*.db";
            this.fdDBFinder.Title = "Selecione o local da db";
            this.fdDBFinder.FileOk += new System.ComponentModel.CancelEventHandler(this.FdDBFinder_FileOk);
            // 
            // btConfiguracoes
            // 
            this.btConfiguracoes.Location = new System.Drawing.Point(430, 254);
            this.btConfiguracoes.Name = "btConfiguracoes";
            this.btConfiguracoes.Size = new System.Drawing.Size(87, 23);
            this.btConfiguracoes.TabIndex = 4;
            this.btConfiguracoes.Text = "Configurações";
            this.btConfiguracoes.UseVisualStyleBackColor = true;
            this.btConfiguracoes.Click += new System.EventHandler(this.BtConfiguracoes_Click);
            // 
            // wbCustomizacao
            // 
            this.wbCustomizacao.Location = new System.Drawing.Point(-15, -11);
            this.wbCustomizacao.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbCustomizacao.Name = "wbCustomizacao";
            this.wbCustomizacao.ScrollBarsEnabled = false;
            this.wbCustomizacao.Size = new System.Drawing.Size(545, 305);
            this.wbCustomizacao.TabIndex = 5;
            // 
            // LauncherGUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::Bot.Properties.Resources.universe;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(528, 289);
            this.Controls.Add(this.btConfiguracoes);
            this.Controls.Add(this.btIniciar);
            this.Controls.Add(this.wbCustomizacao);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.MaximizeBox = false;
            this.Name = "LauncherGUI";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kurosawa Launcher";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.LauncherGUI_FormClosed);
            this.Load += new System.EventHandler(this.LauncherGUI_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btIniciar;
        private System.Windows.Forms.OpenFileDialog fdDBFinder;
        private System.Windows.Forms.Button btConfiguracoes;
        private System.Windows.Forms.WebBrowser wbCustomizacao;
    }
}

