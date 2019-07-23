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
            this.label12 = new System.Windows.Forms.Label();
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
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Url = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Tipo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CBIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Remover = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txUrl = new System.Windows.Forms.TextBox();
            this.btStatusAdicionar = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.cbStatusTipo = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txStatusStatus = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.dgIdiomasLista = new System.Windows.Forms.DataGridView();
            this.IdiomasIdentificador = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdiomasID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdiomasIdioma = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdiomasTexto = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdiomasIndex = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdiomasEditar = new System.Windows.Forms.DataGridViewButtonColumn();
            this.IdiomasRemover = new System.Windows.Forms.DataGridViewButtonColumn();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.btIdiomasSalvar = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.txIdiomasTexto = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.cbIdiomasIdioma = new System.Windows.Forms.ComboBox();
            this.label14 = new System.Windows.Forms.Label();
            this.txIdiomasIdentificador = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label18 = new System.Windows.Forms.Label();
            this.txDblApiToken = new System.Windows.Forms.TextBox();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btPicInicializarSalvar)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStatusEdit)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgIdiomasLista)).BeginInit();
            this.groupBox5.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(397, 549);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.groupBox6);
            this.tabPage1.Controls.Add(this.label12);
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
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(12, 468);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(87, 39);
            this.label12.TabIndex = 5;
            this.label12.Text = "Para Salvar:\r\n\r\nClick na Shiro <3";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(791, 489);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(152, 52);
            this.label9.TabIndex = 4;
            this.label9.Text = "Quem diria não é mesmo...\r\n\r\nNo fim tudo se une de\r\numa forma estranha ¯\\_(ツ)_/¯";
            // 
            // btPicInicializarSalvar
            // 
            this.btPicInicializarSalvar.Image = global::Bot.Properties.Resources.Shiro;
            this.btPicInicializarSalvar.Location = new System.Drawing.Point(6, 282);
            this.btPicInicializarSalvar.Name = "btPicInicializarSalvar";
            this.btPicInicializarSalvar.Size = new System.Drawing.Size(371, 235);
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
            this.btStatusRedefinir.Click += new System.EventHandler(this.BtStatusRedefinir_Click);
            // 
            // btStatusSalvar
            // 
            this.btStatusSalvar.Location = new System.Drawing.Point(308, 494);
            this.btStatusSalvar.Name = "btStatusSalvar";
            this.btStatusSalvar.Size = new System.Drawing.Size(75, 23);
            this.btStatusSalvar.TabIndex = 2;
            this.btStatusSalvar.Text = "Salvar";
            this.btStatusSalvar.UseVisualStyleBackColor = true;
            this.btStatusSalvar.Click += new System.EventHandler(this.BtStatusSalvar_Click);
            // 
            // dtStatusEdit
            // 
            this.dtStatusEdit.AllowUserToAddRows = false;
            this.dtStatusEdit.BackgroundColor = System.Drawing.Color.White;
            this.dtStatusEdit.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtStatusEdit.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Status,
            this.Url,
            this.Tipo,
            this.CBIndex,
            this.Remover});
            this.dtStatusEdit.Location = new System.Drawing.Point(6, 114);
            this.dtStatusEdit.Name = "dtStatusEdit";
            this.dtStatusEdit.Size = new System.Drawing.Size(377, 374);
            this.dtStatusEdit.TabIndex = 1;
            this.dtStatusEdit.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DtStatusEdit_CellContentClick);
            // 
            // Status
            // 
            this.Status.HeaderText = "Status:";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            // 
            // Url
            // 
            this.Url.HeaderText = "Url:";
            this.Url.Name = "Url";
            // 
            // Tipo
            // 
            this.Tipo.HeaderText = "Tipo:";
            this.Tipo.Name = "Tipo";
            this.Tipo.ReadOnly = true;
            this.Tipo.Width = 70;
            // 
            // CBIndex
            // 
            this.CBIndex.HeaderText = "CBIndex";
            this.CBIndex.Name = "CBIndex";
            this.CBIndex.ReadOnly = true;
            this.CBIndex.Visible = false;
            this.CBIndex.Width = 50;
            // 
            // Remover
            // 
            this.Remover.HeaderText = "Remover:";
            this.Remover.Name = "Remover";
            this.Remover.Width = 64;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label13);
            this.groupBox4.Controls.Add(this.txUrl);
            this.groupBox4.Controls.Add(this.btStatusAdicionar);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Controls.Add(this.cbStatusTipo);
            this.groupBox4.Controls.Add(this.label10);
            this.groupBox4.Controls.Add(this.txStatusStatus);
            this.groupBox4.Location = new System.Drawing.Point(6, 6);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(377, 102);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Adicionar Status:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 48);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(23, 13);
            this.label13.TabIndex = 6;
            this.label13.Text = "Url:";
            // 
            // txUrl
            // 
            this.txUrl.Location = new System.Drawing.Point(52, 45);
            this.txUrl.Name = "txUrl";
            this.txUrl.Size = new System.Drawing.Size(319, 20);
            this.txUrl.TabIndex = 5;
            // 
            // btStatusAdicionar
            // 
            this.btStatusAdicionar.Location = new System.Drawing.Point(296, 71);
            this.btStatusAdicionar.Name = "btStatusAdicionar";
            this.btStatusAdicionar.Size = new System.Drawing.Size(75, 23);
            this.btStatusAdicionar.TabIndex = 4;
            this.btStatusAdicionar.Text = "Adicionar";
            this.btStatusAdicionar.UseVisualStyleBackColor = true;
            this.btStatusAdicionar.Click += new System.EventHandler(this.BtStatusAdicionar_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(6, 76);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(31, 13);
            this.label11.TabIndex = 3;
            this.label11.Text = "Tipo:";
            // 
            // cbStatusTipo
            // 
            this.cbStatusTipo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStatusTipo.FormattingEnabled = true;
            this.cbStatusTipo.Items.AddRange(new object[] {
            "Jogando",
            "Live",
            "Ouvindo",
            "Assistindo"});
            this.cbStatusTipo.Location = new System.Drawing.Point(52, 71);
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
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.dgIdiomasLista);
            this.tabPage3.Controls.Add(this.groupBox5);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(389, 523);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Idiomas";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // dgIdiomasLista
            // 
            this.dgIdiomasLista.AllowUserToAddRows = false;
            this.dgIdiomasLista.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgIdiomasLista.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdiomasIdentificador,
            this.IdiomasID,
            this.IdiomasIdioma,
            this.IdiomasTexto,
            this.IdiomasIndex,
            this.IdiomasEditar,
            this.IdiomasRemover});
            this.dgIdiomasLista.Location = new System.Drawing.Point(6, 166);
            this.dgIdiomasLista.Name = "dgIdiomasLista";
            this.dgIdiomasLista.Size = new System.Drawing.Size(377, 351);
            this.dgIdiomasLista.TabIndex = 1;
            this.dgIdiomasLista.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.DgIdiomasLista_CellContentClick);
            // 
            // IdiomasIdentificador
            // 
            this.IdiomasIdentificador.HeaderText = "Identificador";
            this.IdiomasIdentificador.Name = "IdiomasIdentificador";
            this.IdiomasIdentificador.ReadOnly = true;
            // 
            // IdiomasID
            // 
            this.IdiomasID.HeaderText = "ID";
            this.IdiomasID.Name = "IdiomasID";
            this.IdiomasID.ReadOnly = true;
            this.IdiomasID.Visible = false;
            // 
            // IdiomasIdioma
            // 
            this.IdiomasIdioma.HeaderText = "Idioma";
            this.IdiomasIdioma.Name = "IdiomasIdioma";
            this.IdiomasIdioma.ReadOnly = true;
            // 
            // IdiomasTexto
            // 
            this.IdiomasTexto.HeaderText = "Texto";
            this.IdiomasTexto.Name = "IdiomasTexto";
            this.IdiomasTexto.ReadOnly = true;
            // 
            // IdiomasIndex
            // 
            this.IdiomasIndex.HeaderText = "Index";
            this.IdiomasIndex.Name = "IdiomasIndex";
            this.IdiomasIndex.ReadOnly = true;
            this.IdiomasIndex.Visible = false;
            // 
            // IdiomasEditar
            // 
            this.IdiomasEditar.HeaderText = "Editar";
            this.IdiomasEditar.Name = "IdiomasEditar";
            this.IdiomasEditar.ReadOnly = true;
            // 
            // IdiomasRemover
            // 
            this.IdiomasRemover.HeaderText = "Remover";
            this.IdiomasRemover.Name = "IdiomasRemover";
            this.IdiomasRemover.ReadOnly = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.btIdiomasSalvar);
            this.groupBox5.Controls.Add(this.label17);
            this.groupBox5.Controls.Add(this.txIdiomasTexto);
            this.groupBox5.Controls.Add(this.label16);
            this.groupBox5.Controls.Add(this.cbIdiomasIdioma);
            this.groupBox5.Controls.Add(this.label14);
            this.groupBox5.Controls.Add(this.txIdiomasIdentificador);
            this.groupBox5.Location = new System.Drawing.Point(6, 6);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(377, 154);
            this.groupBox5.TabIndex = 0;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Adicionar/Editar:";
            // 
            // btIdiomasSalvar
            // 
            this.btIdiomasSalvar.Location = new System.Drawing.Point(296, 125);
            this.btIdiomasSalvar.Name = "btIdiomasSalvar";
            this.btIdiomasSalvar.Size = new System.Drawing.Size(75, 23);
            this.btIdiomasSalvar.TabIndex = 6;
            this.btIdiomasSalvar.Tag = "-1";
            this.btIdiomasSalvar.Text = "Salvar";
            this.btIdiomasSalvar.UseVisualStyleBackColor = true;
            this.btIdiomasSalvar.Click += new System.EventHandler(this.BtIdiomasSalvar_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(6, 52);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(37, 13);
            this.label17.TabIndex = 5;
            this.label17.Text = "Texto:";
            // 
            // txIdiomasTexto
            // 
            this.txIdiomasTexto.Location = new System.Drawing.Point(9, 68);
            this.txIdiomasTexto.Multiline = true;
            this.txIdiomasTexto.Name = "txIdiomasTexto";
            this.txIdiomasTexto.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txIdiomasTexto.Size = new System.Drawing.Size(362, 51);
            this.txIdiomasTexto.TabIndex = 4;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(192, 22);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(41, 13);
            this.label16.TabIndex = 3;
            this.label16.Text = "Idioma:";
            // 
            // cbIdiomasIdioma
            // 
            this.cbIdiomasIdioma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIdiomasIdioma.FormattingEnabled = true;
            this.cbIdiomasIdioma.Items.AddRange(new object[] {
            "Portugues",
            "Inglês",
            "Espanhol",
            "Russo"});
            this.cbIdiomasIdioma.Location = new System.Drawing.Point(239, 19);
            this.cbIdiomasIdioma.Name = "cbIdiomasIdioma";
            this.cbIdiomasIdioma.Size = new System.Drawing.Size(132, 21);
            this.cbIdiomasIdioma.TabIndex = 2;
            this.cbIdiomasIdioma.SelectedIndexChanged += new System.EventHandler(this.CbIdiomasIdioma_SelectedIndexChanged);
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 22);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(68, 13);
            this.label14.TabIndex = 1;
            this.label14.Text = "Identificador:";
            // 
            // txIdiomasIdentificador
            // 
            this.txIdiomasIdentificador.Location = new System.Drawing.Point(78, 19);
            this.txIdiomasIdentificador.Name = "txIdiomasIdentificador";
            this.txIdiomasIdentificador.Size = new System.Drawing.Size(108, 20);
            this.txIdiomasIdentificador.TabIndex = 0;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(820, 548);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(123, 13);
            this.label15.TabIndex = 1;
            this.label15.Text = "Bot by Takasaki 2k19 ©";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label18);
            this.groupBox6.Controls.Add(this.txDblApiToken);
            this.groupBox6.Location = new System.Drawing.Point(6, 228);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(377, 48);
            this.groupBox6.TabIndex = 3;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "DBL API:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(6, 22);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(41, 13);
            this.label18.TabIndex = 1;
            this.label18.Text = "Token:";
            // 
            // txDblApiToken
            // 
            this.txDblApiToken.Location = new System.Drawing.Point(53, 19);
            this.txDblApiToken.Name = "txDblApiToken";
            this.txDblApiToken.Size = new System.Drawing.Size(318, 20);
            this.txDblApiToken.TabIndex = 0;
            // 
            // ConfiguracoesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Bot.Properties.Resources.kurosawa_dia_love_live_sunshine_minimalism_simple_wallpaper_49f0b8ed81aa4dabe6c7180fd02146cd;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(955, 573);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label9);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfiguracoesForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kurosawa Dia - Configurações";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConfiguracoesForm_FormClosed);
            this.Load += new System.EventHandler(this.ConfiguracoesForm_Load);
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
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgIdiomasLista)).EndInit();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

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
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox txUrl;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.DataGridViewTextBoxColumn Url;
        private System.Windows.Forms.DataGridViewTextBoxColumn Tipo;
        private System.Windows.Forms.DataGridViewTextBoxColumn CBIndex;
        private System.Windows.Forms.DataGridViewButtonColumn Remover;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.DataGridView dgIdiomasLista;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox txIdiomasIdentificador;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Button btIdiomasSalvar;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox txIdiomasTexto;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.ComboBox cbIdiomasIdioma;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdiomasIdentificador;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdiomasID;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdiomasIdioma;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdiomasTexto;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdiomasIndex;
        private System.Windows.Forms.DataGridViewButtonColumn IdiomasEditar;
        private System.Windows.Forms.DataGridViewButtonColumn IdiomasRemover;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox txDblApiToken;
    }
}