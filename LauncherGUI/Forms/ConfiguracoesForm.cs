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
                MessageBox.Show("Dados atualizados com sucesso", "Kurosawa Dia - Tarefa Completa Senpai :D", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void ConfiguracoesForm_Load(object sender, EventArgs e)
        {
            DBDAO dao = new DBDAO();
            var retorno = dao.PegarDadosBot();

            if (retorno.Item1 == 3)
            {
                txBotToken.Text = retorno.Item4.token;
                txBotPrefix.Text = retorno.Item4.prefix;
                txBotIDDono.Text = retorno.Item4.idDono.ToString();

                txDBIP.Text = retorno.Item3.ip;
                txDBDatabase.Text = retorno.Item3.database;
                txDBLogin.Text = retorno.Item3.login;
                txDBSenha.Text = retorno.Item3.senha;

                txWeebAPIToken.Text = retorno.Item2.WeebToken;
            }
        }
    }
}
