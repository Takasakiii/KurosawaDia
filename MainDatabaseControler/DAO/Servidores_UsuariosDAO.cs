using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;

namespace MainDatabaseControler.DAO
{
    public class Servidores_UsuariosDAO
    {
        private MySqlConnection conexao = null;

        public Servidores_UsuariosDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public void inserirServidorUsuario(Servidores_Usuarios servidores_Usuarios)
        {
            const string sql = "call inserirServidor_Usuario(@sid, @snome, @uid, @unome)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@sid", servidores_Usuarios.Servidor.Id);
            cmd.Parameters.AddWithValue("@snome", servidores_Usuarios.Servidor.Nome);
            cmd.Parameters.AddWithValue("@uid", servidores_Usuarios.Usuario.Id);
            cmd.Parameters.AddWithValue("@unome", servidores_Usuarios.Usuario.Nome);

            cmd.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
