using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace MainDatabaseControler.DAO
{
    public class PontosInterativosDAO
    {
        private MySqlConnection conexao = null;

        public PontosInterativosDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool AdicionarPonto(ref PontosInterativos pontosInterativos, ref PI piSaida)
        {
            bool retorno = false;
            const string sql = "call AddPI(@servidor, @usuario)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@servidor", pontosInterativos.Servidores_Usuarios.Servidor.Id);
            cmd.Parameters.AddWithValue("@usuario", pontosInterativos.Servidores_Usuarios.Usuario.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                if (Convert.ToBoolean(rs["Upou"]))
                {
                    pontosInterativos.AddPIInfo(0, Convert.ToUInt64(rs["LevelAtual"]), 0);
                    if (rs["MsgPIUp"] != DBNull.Value)
                    {
                        piSaida = new PI(true, MsgPIUp: (string)rs["MsgPIUp"]);
                        retorno = true;
                    }
                }
            }
            conexao.Close();
            return retorno;
        }
    }
}
