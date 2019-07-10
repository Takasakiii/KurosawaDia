using Bot;
using Bot.Forms;
using Bot.Singletons;
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LauncherGUI
{
    public partial class LauncherGUI : Form
    {
        private const string arquivo = "db.dia";
        public LauncherGUI()
        {
            InitializeComponent();
        }

        private void LauncherGUI_Load(object sender, EventArgs e)
        {
            string curdir = Directory.GetCurrentDirectory();
            wbCustomizacao.Url = new Uri(String.Format("file:///{0}/html/saporra.html", curdir));


            if (File.Exists(arquivo))
            {
                //txtLocal.Text = File.ReadAllText(arquivo);
                btIniciar.Enabled = true; // interface pode melhorar
            }
        }
        private void BtLocal_Click(object sender, EventArgs e)
        {
            fdDBFinder.ShowDialog();
        }
        private void FdDBFinder_FileOk(object sender, CancelEventArgs e)
        {
            //txtLocal.Text = fdDBFinder.FileName;
            btIniciar.Enabled = true;
            if (File.Exists(arquivo))
            {
                File.Delete(arquivo);
            }
            File.Create(arquivo).Close();
            //File.WriteAllText(arquivo, txtLocal.Text);
        }

        private void BtIniciar_Click(object sender, EventArgs e)
        {
            //SingletonConfig.localConfig = txtLocal.Text;

            LogForm log = new LogForm(this);
            SingletonLogs.SetInstance(log, typeof(LogForm));
            log.Show();
            Hide();
            new Thread(() => new Core().IniciarBot()).Start();
        }

        private void LauncherGUI_FormClosed(object sender, FormClosedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BtConfiguracoes_Click(object sender, EventArgs e)
        {
            ConfiguracoesForm config = new ConfiguracoesForm();
            config.ShowDialog();
        }
    }
}
