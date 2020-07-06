using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class ConfiguracoesServidoresDAO
    {
        public async Task<ConfiguracoesServidores> Get(ConfiguracoesServidores config)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            return await context.ConfiguracoesServidores.SingleOrDefaultAsync(x => x.Servidor.ID == config.Servidor.ID && x.Configuracoes == config.Configuracoes);

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    return (await context.ConfiguracoesServidores.FromSqlRaw("call GetServerConfig({0}, {1})", config.Servidor.ID, config.Configuracoes).ToArrayAsync()).FirstOrDefault();
            //}
        }

        public async Task Add(ConfiguracoesServidores config)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            ConfiguracoesServidores configuracoesServidores = await context.ConfiguracoesServidores
                .SingleOrDefaultAsync(x => x.Servidor.ID == config.Servidor.ID && x.Configuracoes == config.Configuracoes);


            
            if (configuracoesServidores == null)
            {
                ConfiguracoesServidores configuracoes = new ConfiguracoesServidores
                {
                    Value = config.Value,
                    Configuracoes = config.Configuracoes,
                    CodServidor = (await context.Servidores.AsNoTracking().FirstOrDefaultAsync(x => x.ID == config.Servidor.ID)).Cod
                };
                await context.ConfiguracoesServidores.AddAsync(configuracoes);
                await context.SaveChangesAsync();
            }
            else
            {
                configuracoesServidores.Value = config.Value;
                context.Update(configuracoesServidores);
                await context.SaveChangesAsync();
            }

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            //    //await context.Database.ExecuteSqlRawAsync("call SetServerConfig ({0}, {1}, {2})", config.Servidor.ID, config.Configuracoes, config.Value);
            //    //_ = context.ConfiguracoesServidores.FromSqlRaw("call SetServerConfig ({0}, {1}, {2})", config.Servidor.ID, config.Configuracoes, config.Value);
            //    //await transation.CommitAsync();

            //    MySqlCommand command = await context.GetMysqlCommand();
            //    command.CommandText = "call SetServerConfig (@si, @c, @v)";
            //    command.Parameters.AddWithValue("@si", config.Servidor.ID);
            //    command.Parameters.AddWithValue("@c", config.Configuracoes);
            //    command.Parameters.AddWithValue("@v", config.Value);
            //    await command.ExecuteNonQueryAsync();
            //}
        }
    }
}
