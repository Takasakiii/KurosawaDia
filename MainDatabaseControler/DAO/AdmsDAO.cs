using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using static MainDatabaseControler.Modelos.Adms;

namespace MainDatabaseControler.DAO
{
    public class AdmsDAO
    {
        private MySqlConnection conexao = null;

        public AdmsDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool GetAdm(ref Adms adms)
        {
            const string sql = "call GetAdm(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", adms.Usuario.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                bool result = Convert.ToBoolean(rs["result"]);
                if (result)
                {
                    adms.SetPerms((PermissoesAdms)rs["permissao"]);
                    retorno = true;
                }
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
