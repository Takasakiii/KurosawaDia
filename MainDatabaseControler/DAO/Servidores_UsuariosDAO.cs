using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace MainDatabaseControler.DAO
{
    public class Servidores_UsuariosDAO
    {
        public async Task inserirServidorUsuarioAsync(Servidores_Usuarios servidores_Usuarios)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call inserirServidor_Usuario(@sid, @snome, @uid, @unome)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@sid", servidores_Usuarios.Servidor.Id);
                cmd.Parameters.AddWithValue("@snome", servidores_Usuarios.Servidor.Nome);
                cmd.Parameters.AddWithValue("@uid", servidores_Usuarios.Usuario.Id);
                cmd.Parameters.AddWithValue("@unome", servidores_Usuarios.Usuario.Nome);

                await cmd.ExecuteNonQueryAsync();
            });
        }
    }
}
