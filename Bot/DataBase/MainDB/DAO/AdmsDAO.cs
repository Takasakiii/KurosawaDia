using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;
using static Bot.DataBase.MainDB.Modelos.Adms;

namespace Bot.DataBase.MainDB.DAO
{
    public class AdmsDAO
    {
        private MySqlConnection conexao;

        public AdmsDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public bool GetAdm(ref Adms adms)
        {
            //privatizando aguarde
            const string sql = "call GetAdm(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", adms.usuario.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                bool result = Convert.ToBoolean(rs["result"]);
                if(result)
                {
                    adms.SetPerms((PermissoesAdm)rs["permissao"]);
                    retorno = true;
                }
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
