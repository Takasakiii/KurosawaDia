using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class ServidoresDAO
    {
        public async Task<Servidores> Get(Servidores servidor)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.Servidores.FromSqlRaw("call GetServidor({0})", servidor.ID).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task Atualizar(Servidores servidor)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("call AtualizarServidor({0}, {1}, {2})", servidor.ID, servidor.Prefix ?? "", servidor.Espercial);
                await transaction.CommitAsync();
            }
        }
    }
}
