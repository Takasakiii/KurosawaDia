using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using static Bot.DataBase.MainDB.Modelos.Servidores;

namespace Bot.DataBase.MainDB.DAO
{
    public class ServidoresDAO
    {
        private MySqlConnection conexao;

        public ServidoresDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public bool GetPrefix(ref Servidores servidor)
        {
            const string sql = "call buscarPrefix(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            string prefix = null;
            if (rs.Read())
            {
                prefix = rs["prefix_servidor"].ToString();
            }
            char[] prefixChar = null;
            bool returno = false;
            if (prefix != "" && prefix != null)
            {
                prefixChar = prefix.ToCharArray();
                servidor.SetPrefix(prefixChar);
                returno = true;
            }

            rs.Close();
            conexao.Close();
            return returno;
        }

        public Servidores SetServidorPrefix(Servidores servidor)
        {
            const string sql = "call atualizarPrefix(@id, @prefix)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.id);
            cmd.Parameters.AddWithValue("@prefix", new string(servidor.prefix));

            MySqlDataReader rs = cmd.ExecuteReader();
            char[] prefix = null;
            if (rs.Read())
            {
                prefix = rs["prefix_servidor"].ToString().ToCharArray();
            }
            rs.Close();
            conexao.Close();
            servidor.SetPrefix(prefix);
            return servidor;
        }

        public bool GetPermissoes(ref Servidores servidor)
        {
            const string sql = "call GetPermissoes(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                servidor.SetPermissao((Permissoes)rs["especial_servidor"]);
                retorno = true;

            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
