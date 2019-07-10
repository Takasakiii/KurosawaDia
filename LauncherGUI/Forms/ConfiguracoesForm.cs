using ConfigurationControler.DAO;
using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class ConfiguracoesForm : Form
    {
        public ConfiguracoesForm()
        {
            InitializeComponent();
        }

        private void BtPicInicializarSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DiaConfig diaConfig = new DiaConfig(txBotToken.Text, txBotPrefix.Text, Convert.ToUInt64(txBotIDDono.Text));
                DBConfig dBConfig = new DBConfig(txDBIP.Text, txDBDatabase.Text, txDBLogin.Text, txDBSenha.Text);
                ApiConfig apiConfig = new ApiConfig(txWeebAPIToken.Text);

                DBDAO dao = new DBDAO();
                dao.AdicionarAtualizar(apiConfig, dBConfig, diaConfig);
            }
            catch (FormatException)
            {
                MessageBox.Show("O campo ID dono so pode conter numeros!!", "Kurosawa Dia - Problemas com os dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (OverflowException)
            {
                MessageBox.Show("O valor do campo ID dono excedeu o limite de dados, verifique se digitou corretamente!", "Kurosawa Dia - Problemas com os dados", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception erro)
            {
                MessageBox.Show("Ops, isso é muito embaraçoso.... Espero que você entenda e corrija XD\n\n" + erro.Message, "Kurosawa Dia - Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
