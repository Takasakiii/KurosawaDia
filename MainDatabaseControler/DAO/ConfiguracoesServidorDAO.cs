using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace MainDatabaseControler.DAO
{
    public class ConfiguracoesServidorDAO
    {
        private MySqlConnection conexao = null;

        public ConfiguracoesServidorDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool SalvarPIConfig(ConfiguracoesServidor config)
        {
            bool retorno = true;
            const string sql = "call configurePI(@id, @enable, @pirate, @msg)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@id", config.servidor.Id);
                cmd.Parameters.AddWithValue("@enable", config.pI.PIConf);
                cmd.Parameters.AddWithValue("@pirate", config.pI.PIRate);
                if (!string.IsNullOrEmpty(config.pI.MsgPIUp))
                {
                    cmd.Parameters.AddWithValue("@msg", config.pI.MsgPIUp);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@msg", DBNull.Value);
                }

                cmd.ExecuteNonQuery();
            }
            catch
            {
                retorno = false;
                throw;
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }

        public void SetWelcomeMsg(ConfiguracoesServidor configuracoes)
        {
            const string sql = "call SetWelcomeMsg(@id, @msg)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
            if (!string.IsNullOrEmpty(configuracoes.bemvindo.bemvindoMsg))
            {
                cmd.Parameters.AddWithValue("@msg", configuracoes.bemvindo.bemvindoMsg);
            }
            else
            {
                cmd.Parameters.AddWithValue("@msg", DBNull.Value);
            }

            cmd.ExecuteNonQuery();
        }

        public bool GetWelcomeMsg(ref ConfiguracoesServidor configuracoes)
        {
            const string sql = "call GetWelcomeMsg(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
            bool retorno = false;
            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                configuracoes = new ConfiguracoesServidor(configuracoes.servidor, new ConfiguracoesServidor.BemVindoGoodByeMsg().setBemvindo((string)rs["bemvindoMsg"]));
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }

        public bool GetByeMsg(ref ConfiguracoesServidor configuracoes)
        {
            const string sql = "call GetByeMsg(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
            bool retorno = false;
            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                if(rs["sairMsg"].GetType() != typeof(DBNull))
                {
                    configuracoes = new ConfiguracoesServidor(configuracoes.servidor, new ConfiguracoesServidor.BemVindoGoodByeMsg().setSair((string)rs["sairMsg"]));
                    retorno = true;
                }
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
