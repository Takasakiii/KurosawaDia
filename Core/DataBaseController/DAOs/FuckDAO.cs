using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataBaseController.DAOs {
    public sealed class FuckDAO {
        public async Task<Fuck> Get(Fuck fuck)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.Fuck.FromSqlRaw("call GetFuck({0})", fuck.Explicit).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task Add(Fuck fuck)
        { 
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("call AddFuck({0}, {1}, {2})", fuck.Usuario.ID, fuck.Url, fuck.Explicit);
                await transaction.CommitAsync();
            }
        }
    }
}
