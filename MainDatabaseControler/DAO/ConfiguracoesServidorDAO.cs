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
    }
}
