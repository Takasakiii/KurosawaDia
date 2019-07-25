using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Bot.Forms
{
    public partial class ConfiguracoesForm : Form
    {
        Form gui = null;
        public ConfiguracoesForm(Form gui)
        {
            InitializeComponent();
            this.gui = gui;
        }

        private void BtPicInicializarSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                DiaConfig diaConfig = new DiaConfig(txBotToken.Text, txBotPrefix.Text, Convert.ToUInt64(txBotIDDono.Text));
                DBConfig dBConfig = new DBConfig(txDBIP.Text, txDBDatabase.Text, txDBLogin.Text, txDBSenha.Text);
                ApisConfig weebApi = new ApisConfig("Weeb", txWeebAPIToken.Text, true, 0);
                ApisConfig dblApi = new ApisConfig("Discord Bot List", txDblApiToken.Text, checkAtualizarDbl.Checked, 1);
                ApisConfig[] apis = new ApisConfig[2];
                apis[0] = weebApi;
                apis[1] = dblApi;
                DBDAO dao = new DBDAO();
                dao.AdicionarAtualizar(apis, dBConfig, diaConfig);
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
            catch (Exception erro)
            {
                MessageBox.Show("Ops, isso é muito embaraçoso.... Espero que você entenda e corrija XD\n\n" + erro.Message, "Kurosawa Dia - Erro Fatal", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfiguracoesForm_Load(object sender, EventArgs e)
        {
            DBDAO dao = new DBDAO();
            var retorno = dao.PegarDadosBot();

            
                if (retorno.Item4 != null)
                {
                    txBotToken.Text = retorno.Item4.token;
                    txBotPrefix.Text = retorno.Item4.prefix;
                    txBotIDDono.Text = retorno.Item4.idDono.ToString();
                }

                
                if (retorno.Item3 != null)
                {
                    txDBIP.Text = retorno.Item3.ip;
                    txDBDatabase.Text = retorno.Item3.database;
                    txDBLogin.Text = retorno.Item3.login;
                    txDBSenha.Text = retorno.Item3.senha;
                }

                
                if(retorno.Item2 != null)
                {
                    txWeebAPIToken.Text = retorno.Item2[0].Token;
                    txDblApiToken.Text = retorno.Item2[1].Token;
                    checkAtualizarDbl.Checked = retorno.Item2[1].Ativada;
                }

            StatusDAO sdao = new StatusDAO();
            var retorno2 = sdao.CarregarStatus();

            if (retorno2.Item1)
            {
                foreach (Status status in retorno2.Item2)
                {
                    dtStatusEdit.Rows.Add(status.status_jogo, status.status_url, status.status_tipo, (int)status.status_tipo);
                }
            }
        }


        private void BtStatusAdicionar_Click(object sender, EventArgs e)
        {
            if (txStatusStatus.Text != null && cbStatusTipo.SelectedIndex >= 0)
            {
                dtStatusEdit.Rows.Add(txStatusStatus.Text, txUrl.Text, cbStatusTipo.Text, cbStatusTipo.SelectedIndex);
                txStatusStatus.Clear();
                txUrl.Clear();
                txStatusStatus.Focus();
                cbStatusTipo.SelectedIndex = -1;
            }
        }

        private void DtStatusEdit_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (dg.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewRow row = dtStatusEdit.Rows[e.RowIndex];
                dtStatusEdit.Rows.Remove(row);
            }
        }

        private void BtStatusRedefinir_Click(object sender, EventArgs e)
        {
            dtStatusEdit.Rows.Clear();
            new StatusDAO().RemoverTabela();
        }

        private void BtStatusSalvar_Click(object sender, EventArgs e)
        {
            if (dtStatusEdit.Rows.Count > 0)
            {
                List<Status> statuses = new List<Status>();
                for (int i = 0; i < dtStatusEdit.RowCount; i++)
                {
                    Status temp = new Status(dtStatusEdit.Rows[i].Cells[0].Value.ToString(), (Status.TiposDeStatus)Convert.ToInt32(dtStatusEdit.Rows[i].Cells[3].Value), dtStatusEdit.Rows[i].Cells[1].Value.ToString());
                    statuses.Add(temp);
                }

                StatusDAO dao = new StatusDAO();
                dao.AdicionarAtualizarStatus(statuses);
                MessageBox.Show("Dados atualizados com sucesso", "Kurosawa Dia - Tarefa Completa Senpai :D", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BtIdiomasSalvar_Click(object sender, EventArgs e)
        {
            if(btIdiomasSalvar.Tag.ToString() == "-1")
            {
                if (cbIdiomasIdioma.SelectedIndex >= 0 && txIdiomasIdentificador.Text != "" && txIdiomasTexto.Text != "")
                {
                    Linguagens linguagem = new Linguagens((Linguagens.Idiomas)cbIdiomasIdioma.SelectedIndex, txIdiomasIdentificador.Text, txIdiomasTexto.Text);
                    LinguagensDAO dao = new LinguagensDAO();
                    dao.Adicionar(linguagem);
                    CbIdiomasIdioma_SelectedIndexChanged(null, null);
                    Limpar();
                }
            }
            else
            {
                Linguagens linguagens = new Linguagens ((Linguagens.Idiomas)cbIdiomasIdioma.SelectedIndex, txIdiomasIdentificador.Text, txIdiomasTexto.Text, Convert.ToUInt32(btIdiomasSalvar.Tag));
                LinguagensDAO dao = new LinguagensDAO();
                dao.Atualisar(linguagens);

                btIdiomasSalvar.Tag = "-1";
                CbIdiomasIdioma_SelectedIndexChanged(null, null);
                Limpar();
            }
        }

        private void Limpar()
        {
            txIdiomasIdentificador.Clear();
            txIdiomasTexto.Clear();
            txIdiomasIdentificador.Focus();
        }

        private void CbIdiomasIdioma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbIdiomasIdioma.SelectedIndex >= 0)
            {
                dgIdiomasLista.Rows.Clear();

                Linguagens linguagens = new Linguagens((Linguagens.Idiomas)cbIdiomasIdioma.SelectedIndex);
                LinguagensDAO dao = new LinguagensDAO();
                var retorno = dao.Listar(linguagens);
                if (retorno.Item1)
                {
                    foreach(Linguagens lin in retorno.Item2)
                    {
                        dgIdiomasLista.Rows.Add(lin.stringIdentifier, lin.idString, lin.idiomaString.ToString(), lin.texto, (int)lin.idiomaString);
                    }
                }
            }
        }

        private void DgIdiomasLista_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dg = (DataGridView)sender;
            if (dg.Columns[e.ColumnIndex] is DataGridViewButtonColumn && e.RowIndex >= 0)
            {
                DataGridViewButtonColumn botaoColuna = (DataGridViewButtonColumn)dg.Columns[e.ColumnIndex];
                if(botaoColuna.Name == "IdiomasRemover")
                {
                    DataGridViewRow row = dgIdiomasLista.Rows[e.RowIndex];
                    Linguagens linguagens = new Linguagens(idString: Convert.ToUInt64(row.Cells["IdiomasID"].Value));
                    LinguagensDAO dao = new LinguagensDAO();
                    dao.Deletar(linguagens);
                    dgIdiomasLista.Rows.Remove(row);
                }
                else
                {
                    if(botaoColuna.Name == "IdiomasEditar")
                    {
                        DataGridViewRow row = dgIdiomasLista.Rows[e.RowIndex];
                        txIdiomasIdentificador.Text = row.Cells["IdiomasIdentificador"].Value.ToString();
                        txIdiomasTexto.Text = row.Cells["IdiomasTexto"].Value.ToString();
                        cbIdiomasIdioma.SelectedIndex = Convert.ToInt32(row.Cells["IdiomasIndex"].Value);
                        btIdiomasSalvar.Tag = Convert.ToInt32(row.Cells["IdiomasID"].Value);
                    }
                }

                
            }
        }

        private void ConfiguracoesForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            gui.Show();
        }
    }
}
