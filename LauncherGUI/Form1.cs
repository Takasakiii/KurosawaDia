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

        public Form1()
        {
            InitializeComponent();
        }

        private void btIniciar_Click(object sender, EventArgs e)
        {
            t = new Thread(Process);
            t.Start();

            btIniciar.Enabled = false;
            txtToken.Enabled = false;

            MessageBox.Show("Bot Iniciado");
        }

        private void Process()
        {
            if(txtToken.Text != null)
            {
                new Bot.Core().Iniciar(txtToken.Text);
            } else
            {
                MessageBox.Show("O token eh invalido");
            }
        }
    }
}
