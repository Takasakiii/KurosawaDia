using Bot;
using Bot.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LauncherGUI
{
    public partial class LauncherGUI : Form
    {
        private const string nomeArquivo = "dblocal.ayura"; // constande de facepalm
        private Thread botThread;
        public LauncherGUI()
        {
            InitializeComponent();
        }

        private void LauncherGUI_Load(object sender, EventArgs e)
        {
            if (File.Exists(nomeArquivo))
            {
                txtLocal.Text = File.ReadAllText(nomeArquivo);
                btIniciar.Enabled = true;
            }
        }

        private void BtLocal_Click(object sender, EventArgs e)
        {
            fdDBFinder.ShowDialog();
        }

        private void fdDBFinder_FileOk(object sender, CancelEventArgs e)
        {
            //refaz
            if (!fdDBFinder.FileName.EndsWith(".db"))
            {
                MessageBox.Show("Essa não é um db válida");
            }
            else
            {
                txtLocal.Text = fdDBFinder.FileName;
                btIniciar.Enabled = true;
                if (File.Exists(nomeArquivo))
                {
                    File.Delete(nomeArquivo);
                }
                File.Create(nomeArquivo).Close();
                File.WriteAllText(nomeArquivo, txtLocal.Text);
            }
        }

        private void BtIniciar_Click(object sender, EventArgs e)
        {
            SingletonConfig.localConfig = txtLocal.Text;
            try
            {
                botThread = new Thread(() => new Core().IniciarBot());
                botThread.Start();

                btIniciar.Enabled = false;
                MessageBox.Show("O bot foi iniciado");
            }
            catch (Exception erro)
            {
                MessageBox.Show($"Não foi possivel iniciar o bot: {erro}");
            }
        }

        private void LauncherGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            MessageBox.Show("Todos os processos foram encerrados");
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

    }
}
