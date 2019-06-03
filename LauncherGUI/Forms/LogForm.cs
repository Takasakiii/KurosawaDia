using System;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class LogForm : Form
    {
        public LogForm()
        {
            InitializeComponent();
        }

        public void Log(string e)
        {
            try
            {
                if(Created)
                {
                    txLog.Invoke((MethodInvoker)delegate
                    {
                        txLog.Text += $"\r\n  {e}";

                    });
                }
            }
            catch (ObjectDisposedException)
            {
                MessageBox.Show("O Bot foi desligado");
            }
        }

        public void Fechar()
        {
            Close();
        }

        private void BtLimpar_Click(object sender, EventArgs e)
        {
            txLog.Text = "";
        }
    }
}
