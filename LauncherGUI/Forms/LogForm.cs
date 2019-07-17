using Bot.Singletons;
using System;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class LogForm : Form
    {
        Form Launcher;
        public LogForm(Form Launcher)
        {
            InitializeComponent();
            this.Launcher = Launcher;
        }

        public void Log(string e)
        {
            try
            {
                if (Created)
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

        private void BtLimpar_Click(object sender, EventArgs e)
        {
            txLog.Text = "";
        }

        private void BtDesligar_Click(object sender, EventArgs e)
        {
            SingletonClient.client.StopAsync().GetAwaiter().GetResult();
            SingletonClient.setNull();
            Launcher.Show();
            Close();
        }

        private void VerLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
        }

        private void LogForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            SingletonClient.client.StopAsync().GetAwaiter().GetResult();
            SingletonClient.setNull();
            Launcher.Show();
        }

        private void SairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void LogForm_Resize(object sender, EventArgs e)
        {
            if (((Form)sender).WindowState != FormWindowState.Normal)
            {
                WindowState = FormWindowState.Minimized;
                ShowInTaskbar = false;
            }
        }
    }
}
