using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.MainDB.DAO
{
    public class ConfiguracoesServidorDAO
    {
        private MySqlConnection conexao = new MySqlConstructor().Conectar();

        public bool SalvarPIConfig (ConfiguracoesServidor config)
        {
            bool retorno = true;
            const string sql = "call configurePI(@id, @enable, @pirate, @msg)";
            try
            {
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@id", config.servidor.id);
                cmd.Parameters.AddWithValue("@enable", config.pI.PIConf);
                cmd.Parameters.AddWithValue("@pirate", config.pI.PIRate);
                if (!string.IsNullOrEmpty(config.pI.MsgPIUp))
                {
                    cmd.Parameters.AddWithValue("@msg", config.pI.MsgPIUp);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@msg", "");
                }

                cmd.ExecuteNonQuery();
            }
            catch
            {
                retorno = false;
            }
            finally
            {
                conexao.Close();
            }
            return retorno;
        }
    }
}
