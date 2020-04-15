using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public class CanaisDAO
    {
        public async Task<Canais> Get(Canais canal)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.Canais.FromSqlRaw("call GetCanal({0}, {1})", canal.Servidor.ID, canal.TipoCanal).ToListAsync()).FirstOrDefault();
            }
        }

        public void Adicionar(Canais canal)
        {
            using(Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
                //await context.Database.ExecuteSqlRawAsync("call AddCanal({0}, {1}, {2}, {3})", canal.TipoCanal, canal.Nome, canal.ID, canal.Servidor.ID);
                _ = context.Canais.FromSqlRaw("call AddCanal({0}, {1}, {2}, {3})", canal.TipoCanal, canal.Nome, canal.ID, canal.Servidor.ID);
                //await transation.CommitAsync();
            }
        }
    }
}
