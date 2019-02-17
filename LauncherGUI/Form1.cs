using Bot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LauncherGUI
{
    public partial class Form1 : Form
    {
        private Thread t;
        Core core = new Core();

        public Form1()
        {
            InitializeComponent();
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            t = new Thread(Process);
            t.Start();

            MessageBox.Show("Bot Iniciado");

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
        }
    }
}
