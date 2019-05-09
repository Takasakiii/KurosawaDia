using Bot;
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
        private const string arquivo = "db.ayura"; //aki nego sabe o q eh constante 
        public LauncherGUI()
        {
            InitializeComponent();
        }

        private void LauncherGUI_Load(object sender, EventArgs e)
        {
            if (File.Exists(arquivo))
            {
                txtLocal.Text = File.ReadAllText(arquivo);
                btIniciar.Enabled = true; // interface pode melhorar
            }
        }
        private void BtLocal_Click(object sender, EventArgs e)
        {
            fdDBFinder.ShowDialog();
        }
        private void FdDBFinder_FileOk(object sender, CancelEventArgs e)
        {
            txtLocal.Text = fdDBFinder.FileName;
            btIniciar.Enabled = true;
            if (File.Exists(arquivo))
            {
                File.Delete(arquivo);
            }
            File.Create(arquivo).Close();
            File.WriteAllText(arquivo, txtLocal.Text);
        }

        private void BtIniciar_Click(object sender, EventArgs e)
        {
            SingletonConfig.localConfig = txtLocal.Text;

            new Thread(() => new Core().IniciarBot()).Start();
            btIniciar.Enabled = false;
            MessageBox.Show("O Bot foi iniciado");
        }
    }
}
