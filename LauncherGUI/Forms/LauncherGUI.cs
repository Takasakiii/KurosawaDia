using Bot;
using Bot.Extensions;
using Bot.Forms;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Singletons;
using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace Bot.Forms
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
            //isso aki eh uma gambiarra fudida, pls consertar pq se n fudeu no futuro Xis De



            LogForm log = new LogForm(this);
            LogEmiter.SetMetodoLog(log.Log);
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
            ConfiguracoesForm config = new ConfiguracoesForm(this);
            Hide();
            config.ShowDialog();
        }

        private void CheckButton()
        {
            if (File.Exists(DB.localDB) && new DBDAO().PegarDadosBot().Item1 == 3)
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
