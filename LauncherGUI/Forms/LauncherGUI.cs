using Bot;
using Bot.Forms;
using Bot.Singletons;
using ConfigurationControler.Singletons;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace LauncherGUI
{
    public partial class LauncherGUI : Form
    {
        
        public LauncherGUI()
        {
            InitializeComponent();
        }

        private void LauncherGUI_Load(object sender, EventArgs e)
        {
            string curdir = Directory.GetCurrentDirectory();
            wbCustomizacao.Url = new Uri(String.Format("file:///{0}/html/saporra.html", curdir));

            CheckButton();
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

        private void CheckButton()
        {
            if (File.Exists(DB.localDB))
            {
                btIniciar.Enabled = true;
            }
            else
            {
                btIniciar.Enabled = false;
            }
        }
    }
}
