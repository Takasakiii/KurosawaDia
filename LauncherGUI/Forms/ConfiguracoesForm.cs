using ConfigurationControler.Factory;
using ConfigurationControler.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class ConfiguracoesForm : Form
    {
        public ConfiguracoesForm()
        {
            InitializeComponent();
        }

        private void BtPicInicializarSalvar_Click(object sender, EventArgs e)
        {
            DB.SetDB("pitasgay.db");
            new ConnectionFactory().Conectar();
        }
    }
}
