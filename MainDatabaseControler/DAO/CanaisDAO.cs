using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace MainDatabaseControler.DAO
{
    public class CanaisDAO
    {
        private MySqlConnection conexao = null;

        public CanaisDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool AddCh(Canais canal)
        {
            const string sql = "call setCh(@tipo, @nome, @id, @idServidor)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@tipo", (int)canal.TipoCanal);
            cmd.Parameters.AddWithValue("@nome", canal.NomeCanal);
            cmd.Parameters.AddWithValue("@id", canal.Id);
            cmd.Parameters.AddWithValue("@idServidor", canal.Servidor.Id);

            bool retorno = false;
            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                retorno = Convert.ToBoolean(rs["result"]);
            }

            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
