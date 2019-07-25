using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;

namespace Bot.DataBase.MainDB.DAO
{
    public class Servidores_UsuariosDAO
    {
        private MySqlConnection conexao = new MySqlConstructor().Conectar();

        public void inserirServidorUsuario(Servidores_Usuarios servidores_Usuarios)
        {
            const string sql = "call inserirServidor_Usuario(@sid, @snome, @uid, @unome)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@sid", servidores_Usuarios.servidor.id);
            cmd.Parameters.AddWithValue("@snome", servidores_Usuarios.servidor.nome);
            cmd.Parameters.AddWithValue("@uid", servidores_Usuarios.usuario.id);
            cmd.Parameters.AddWithValue("@unome", servidores_Usuarios.usuario.nome);

            cmd.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
