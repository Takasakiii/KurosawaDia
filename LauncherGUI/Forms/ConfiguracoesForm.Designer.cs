namespace Bot.Forms
{
    partial class ConfiguracoesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfiguracoesForm));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.label9 = new System.Windows.Forms.Label();
            this.btPicInicializarSalvar = new System.Windows.Forms.PictureBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txWeebAPIToken = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txDBSenha = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txDBLogin = new System.Windows.Forms.TextBox();
            this.txDBDatabase = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txDBIP = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txBotIDDono = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txBotPrefix = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txBotToken = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btStatusRedefinir = new System.Windows.Forms.Button();
            this.btStatusSalvar = new System.Windows.Forms.Button();
            this.dtStatusEdit = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btStatusAdicionar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbStatusTipo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txStatusStatus = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btPicInicializarSalvar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStatusEdit)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(397, 549);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.label12);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.btPicInicializarSalvar);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(389, 523);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Inicialização";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(231, 277);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 52);
            this.label9.TabIndex = 4;
            this.label9.Text = "Quem diria não é mesmo...\r\n\r\nNo fim tudo se une de\r\numa forma estranha ¯\\_(ツ)_/¯";
            // 
            // btPicInicializarSalvar
            // 
            this.btPicInicializarSalvar.Image = global::Bot.Properties.Resources.Shiro;
            this.btPicInicializarSalvar.Location = new System.Drawing.Point(6, 237);
            this.btPicInicializarSalvar.Name = "btPicInicializarSalvar";
            this.btPicInicializarSalvar.Size = new System.Drawing.Size(371, 280);
            this.btPicInicializarSalvar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.btPicInicializarSalvar.TabIndex = 3;
            this.btPicInicializarSalvar.TabStop = false;
            this.btPicInicializarSalvar.Click += new System.EventHandler(this.BtPicInicializarSalvar_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.txWeebAPIToken);
            this.groupBox3.Location = new System.Drawing.Point(6, 174);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(377, 48);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Weeb API:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(41, 13);
            this.label8.TabIndex = 1;
            this.label8.Text = "Token:";
            // 
            // txWeebAPIToken
            // 
            this.txWeebAPIToken.Location = new System.Drawing.Point(53, 19);
            this.txWeebAPIToken.Name = "txWeebAPIToken";
            this.txWeebAPIToken.Size = new System.Drawing.Size(318, 20);
            this.txWeebAPIToken.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txDBSenha);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txDBLogin);
            this.groupBox2.Controls.Add(this.txDBDatabase);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txDBIP);
            this.groupBox2.Location = new System.Drawing.Point(6, 90);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(377, 78);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Banco de Dados:";
            // 
            // txDBSenha
            // 
            this.txDBSenha.Location = new System.Drawing.Point(242, 45);
            this.txDBSenha.Name = "txDBSenha";
            this.txDBSenha.Size = new System.Drawing.Size(129, 20);
            this.txDBSenha.TabIndex = 7;
            this.txDBSenha.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(180, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 6;
            this.label7.Text = "Senha:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 48);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Login:";
            // 
            // txDBLogin
            // 
            this.txDBLogin.Location = new System.Drawing.Point(53, 45);
            this.txDBLogin.Name = "txDBLogin";
            this.txDBLogin.Size = new System.Drawing.Size(121, 20);
            this.txDBLogin.TabIndex = 4;
            // 
            // txDBDatabase
            // 
            this.txDBDatabase.Location = new System.Drawing.Point(242, 19);
            this.txDBDatabase.Name = "txDBDatabase";
            this.txDBDatabase.Size = new System.Drawing.Size(129, 20);
            this.txDBDatabase.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(180, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Database:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(20, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "IP:";
            // 
            // txDBIP
            // 
            this.txDBIP.Location = new System.Drawing.Point(53, 19);
            this.txDBIP.Name = "txDBIP";
            this.txDBIP.Size = new System.Drawing.Size(121, 20);
            this.txDBIP.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txBotIDDono);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txBotPrefix);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txBotToken);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(377, 78);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bot:";
            // 
            // txBotIDDono
            // 
            this.txBotIDDono.Location = new System.Drawing.Point(203, 45);
            this.txBotIDDono.Name = "txBotIDDono";
            this.txBotIDDono.Size = new System.Drawing.Size(168, 20);
            this.txBotIDDono.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(132, 48);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "ID do Dono:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(36, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Prefix:";
            // 
            // txBotPrefix
            // 
            this.txBotPrefix.Location = new System.Drawing.Point(53, 45);
            this.txBotPrefix.Name = "txBotPrefix";
            this.txBotPrefix.Size = new System.Drawing.Size(73, 20);
            this.txBotPrefix.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Token:";
            // 
            // txBotToken
            // 
            this.txBotToken.Location = new System.Drawing.Point(53, 19);
            this.txBotToken.Name = "txBotToken";
            this.txBotToken.Size = new System.Drawing.Size(318, 20);
            this.txBotToken.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btStatusRedefinir);
            this.tabPage2.Controls.Add(this.btStatusSalvar);
            this.tabPage2.Controls.Add(this.dtStatusEdit);
            this.tabPage2.Controls.Add(this.groupBox4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(389, 523);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Status";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btStatusRedefinir
            // 
            this.btStatusRedefinir.Location = new System.Drawing.Point(227, 494);
            this.btStatusRedefinir.Name = "btStatusRedefinir";
            this.btStatusRedefinir.Size = new System.Drawing.Size(75, 23);
            this.btStatusRedefinir.TabIndex = 3;
            this.btStatusRedefinir.Text = "Redefinir";
            this.btStatusRedefinir.UseVisualStyleBackColor = true;
            // 
            // btStatusSalvar
            // 
            this.btStatusSalvar.Location = new System.Drawing.Point(308, 494);
            this.btStatusSalvar.Name = "btStatusSalvar";
            this.btStatusSalvar.Size = new System.Drawing.Size(75, 23);
            this.btStatusSalvar.TabIndex = 2;
            this.btStatusSalvar.Text = "Salvar";
            this.btStatusSalvar.UseVisualStyleBackColor = true;
            // 
            // dtStatusEdit
            // 
            this.dtStatusEdit.BackgroundColor = System.Drawing.Color.White;
            this.dtStatusEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtStatusEdit.Location = new System.Drawing.Point(6, 93);
            this.dtStatusEdit.Name = "dtStatusEdit";
            this.dtStatusEdit.Size = new System.Drawing.Size(377, 395);
            this.dtStatusEdit.TabIndex = 1;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.btStatusAdicionar);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.cbStatusTipo);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txStatusStatus);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(377, 81);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adicionar Status:";
            // 
            // btStatusAdicionar
            // 
            this.btStatusAdicionar.Location = new System.Drawing.Point(296, 45);
            this.btStatusAdicionar.Name = "btStatusAdicionar";
            this.btStatusAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btStatusAdicionar.TabIndex = 4;
            this.btStatusAdicionar.Text = "Adicionar";
            this.btStatusAdicionar.UseVisualStyleBackColor = true;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 48);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Tipo:";
            // 
            // cbStatusTipo
            // 
            this.cbStatusTipo.FormattingEnabled = true;
            this.cbStatusTipo.Location = new System.Drawing.Point(52, 45);
            this.cbStatusTipo.Name = "cbStatusTipo";
            this.cbStatusTipo.Size = new System.Drawing.Size(238, 21);
            this.cbStatusTipo.TabIndex = 2;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(40, 13);
            this.label10.TabIndex = 1;
            this.label10.Text = "Status:";
            // 
            // txStatusStatus
            // 
            this.txStatusStatus.Location = new System.Drawing.Point(52, 19);
            this.txStatusStatus.Name = "txStatusStatus";
            this.txStatusStatus.Size = new System.Drawing.Size(319, 20);
            this.txStatusStatus.TabIndex = 0;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 468);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 39);
            this.label12.TabIndex = 5;
            this.label12.Text = "Para Salvar:\r\n\r\nClick na Shiro <3";
            // 
            // ConfiguracoesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Bot.Properties.Resources.kurosawa_dia_love_live_sunshine_minimalism_simple_wallpaper_49f0b8ed81aa4dabe6c7180fd02146cd;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(955, 573);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracoesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kurosawa Dia - Configurações";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btPicInicializarSalvar)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtStatusEdit)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.PictureBox btPicInicializarSalvar;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txWeebAPIToken;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txDBSenha;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txDBLogin;
        private System.Windows.Forms.TextBox txDBDatabase;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txDBIP;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txBotIDDono;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txBotPrefix;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txBotToken;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridView dtStatusEdit;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btStatusAdicionar;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox cbStatusTipo;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txStatusStatus;
        private System.Windows.Forms.Button btStatusRedefinir;
        private System.Windows.Forms.Button btStatusSalvar;
        private System.Windows.Forms.Label label12;
    }
}