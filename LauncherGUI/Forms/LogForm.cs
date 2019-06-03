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
                    textBox1.Invoke((MethodInvoker)delegate
                    {
                        textBox1.Text += $"\r\n  {e}";

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
    }
}
