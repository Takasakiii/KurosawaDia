using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using static Bot.DataBase.MainDB.Modelos.ConfiguracoesServidor;

namespace Bot.DataBase.MainDB.DAO
{
    public class PontosInterativosDAO
    {
        private MySqlConnection conexao = new MySqlConstructor().Conectar();

        public bool AdicionarPonto (PontosInterativos pontosInterativos, ref PI piSaida)
        {
            bool retorno = false;
            const string sql = "call AddPI(@servidor, @usuario)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@servidor", pontosInterativos.servidores_usuarios.servidor.id);
            cmd.Parameters.AddWithValue("@usuario", pontosInterativos.servidores_usuarios.usuario.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                if (Convert.ToBoolean(rs["Upou"]))
                {
                    if(rs["MsgPIUp"] != null)
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
