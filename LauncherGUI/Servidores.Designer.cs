namespace Bot
{
    partial class Servidores
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Servidores));
            this.dataServidores = new System.Windows.Forms.DataGridView();
            this.Servers = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.servers_id = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataServidores)).BeginInit();
            this.SuspendLayout();
            // 
            // dataServidores
            // 
            this.dataServidores.AccessibleName = "";
            this.dataServidores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataServidores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataServidores.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Servers,
            this.servers_id});
            this.dataServidores.Location = new System.Drawing.Point(0, 0);
            this.dataServidores.Name = "dataServidores";
            this.dataServidores.Size = new System.Drawing.Size(800, 450);
            this.dataServidores.TabIndex = 1;
            // 
            // Servers
            // 
            this.Servers.HeaderText = "Servidores:";
            this.Servers.Name = "Servers";
            this.Servers.ReadOnly = true;
            // 
            // servers_id
            // 
            this.servers_id.HeaderText = "Ids:";
            this.servers_id.Name = "servers_id";
            this.servers_id.ReadOnly = true;
            // 
            // Servidores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dataServidores);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Servidores";
            this.Text = "Servidores";
            this.Load += new System.EventHandler(this.Servidores_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataServidores)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataServidores;
        private System.Windows.Forms.DataGridViewTextBoxColumn Servers;
        private System.Windows.Forms.DataGridViewTextBoxColumn servers_id;
    }
}