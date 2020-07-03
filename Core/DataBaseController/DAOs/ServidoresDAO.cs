using DataBaseController.Abstractions;
using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class ServidoresDAO
    {
        public async Task<Servidores> Get(Servidores servidor)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            return await context.Servidores.SingleOrDefaultAsync(x => x.ID == servidor.ID);

            //return (await context.Servidores.FromSqlRaw("call GetServidor({0})", servidor.ID).ToListAsync()).FirstOrDefault();
        }

        public async Task Atualizar(Servidores s)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            Servidores servidores = await context.Servidores.SingleOrDefaultAsync(x => x.ID == s.ID);

            if (s.Prefix != "")
            {
                servidores.Prefix = s.Prefix;
            }
            if (s.Especial != TiposServidores.Inalterado)
            {
                servidores.Especial = s.Especial;
            }

            await context.SaveChangesAsync();

            //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            //await context.Database.ExecuteSqlRawAsync("call AtualizarServidor({0}, {1}, {2})", servidor.ID, servidor.Prefix ?? "", servidor.Especial);
            //await context.Servidores.FromSqlRaw("call AtualizarServidor({0}, {1}, {2})", servidor.ID, servidor.Prefix ?? "", servidor.Especial).LoadAsync();
            //_ = context.Set<Void>().FromSqlRaw("call AtualizarServidor({0}, {1}, {2})", servidor.ID, servidor.Prefix ?? "", servidor.Especial).LoadAsync();
            //await context.SaveChangesAsync();
            //await transation.CommitAsync();

            //MySqlCommand command = await context.GetMysqlCommand();
            //command.CommandText = "call AtualizarServidor(@si, @sp, @se)";
            //command.Parameters.AddWithValue("@si", servidor.ID);
            //command.Parameters.AddWithValue("@sp", servidor.Prefix ?? "");
            //command.Parameters.AddWithValue("@se", servidor.Especial);
            //await command.ExecuteNonQueryAsync();
        }
    }
}
