using Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LauncherGUI
{
    public partial class frGUI : Form
    {
        private Thread t;
        Core core = new Core();

        public frGUI()
        {
            InitializeComponent();
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            t = new Thread(Process);
            t.Start();

            MessageBox.Show("O Bot Iniciado");

            GUI(false);
        }

        private void Process()
        {
            if(txtToken.Text != null && txtPrefix.Text != null && txtWeeb != null)
            {
               core.Iniciar(txtToken.Text, txtPrefix.Text, txtWeeb.Text);
            } else
            {
                MessageBox.Show("O token ou o prefixo eh invalido");
            }
        }

        private void GUI(bool tipo)
        {
            btIniciar.Enabled = tipo;
            btDesligar.Enabled = !tipo;
            txtToken.Enabled = tipo;
            txtPrefix.Enabled = tipo;
            txtWeeb.Enabled = tipo;
        }

        private void btDesligar_Click(object sender, EventArgs e)
        {
            GUI(true);
            core.DesligarAsync();
            t.Abort();
           if(t.IsAlive == true)
            {
                MessageBox.Show("A thred não foi Desligada");
            } else
            {
                MessageBox.Show("O Bot foi Desligado");
            }
        }
    }
}
