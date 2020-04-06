using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class Usuarios_ServidoresDAO
    {
        public async Task Add(Servidores_Usuarios su)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                //await context.Database.BeginTransactionAsync();
                //await context.Database.ExecuteSqlRawAsync($"call CadastrarUsuarioServidor({su.Servidor.ID}, {su.Usuario.ID}, {su.Servidor.Nome}, {su.Usuario.Nome})");
                MySqlCommand cmd = await context.GetMysqlCommand();
                cmd.CommandText = "call CadastrarUsuarioServidor(@si, @ui, @sn, @un)";
                cmd.Parameters.AddWithValue("@si", su.Servidor.ID);
                cmd.Parameters.AddWithValue("@ui", su.Usuario.ID);
                cmd.Parameters.AddWithValue("@sn", su.Servidor.Nome);
                cmd.Parameters.AddWithValue("@un", su.Usuario.Nome);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}
