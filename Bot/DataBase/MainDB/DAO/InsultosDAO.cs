using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace Bot.DataBase.MainDB.DAO
{
    public class InsultosDAO
    {
        private MySqlConnection conexao = null;
        public InsultosDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public bool InserirInsulto(Insultos insulto)
        {
            try
            {
                const string sql = "call AdicionarInsulto(@id, @insulto)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", insulto.usuario.id);
                cmd.Parameters.AddWithValue("@insulto", insulto.insulto);

                cmd.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool GetInsulto(ref Insultos insulto)
        {
            const string sql = "call PegarInsulto()";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            MySqlDataReader rs = cmd.ExecuteReader();

            bool retorno = false;
            if (rs.Read())
            {
                Usuarios usuario = new Usuarios(Convert.ToUInt64(rs["id_usuario"]), (string)rs["nome_usuario"]);
                insulto.SetInsulto((string)rs["insulto"], usuario, Convert.ToUInt32(rs["cod"]));
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
