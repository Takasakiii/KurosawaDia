using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class ConfiguracoesServidoresDAO
    {
        public async Task<ConfiguracoesServidores> Get(ConfiguracoesServidores config)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.ConfiguracoesServidores.FromSqlRaw("call GetServerConfig({0}, {1})", config.Servidor.Cod, config.Configuracoes).ToArrayAsync()).FirstOrDefault();
            }
        }

        public async Task Add(ConfiguracoesServidores config)
        {
            using(Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
                //await context.Database.ExecuteSqlRawAsync("call SetServerConfig ({0}, {1}, {2})", config.Servidor.ID, config.Configuracoes, config.Value);
                //_ = context.ConfiguracoesServidores.FromSqlRaw("call SetServerConfig ({0}, {1}, {2})", config.Servidor.ID, config.Configuracoes, config.Value);
                //await transation.CommitAsync();

                MySqlCommand command = await context.GetMysqlCommand();
                command.CommandText = "call SetServerConfig (@si, @c, @v)";
                command.Parameters.AddWithValue("@si", config.Servidor.Cod);
                command.Parameters.AddWithValue("@c", config.Configuracoes);
                command.Parameters.AddWithValue("@v", config.Value);
                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
