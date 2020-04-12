using DataBaseController.Contexts;
using DataBaseController.Modelos;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.EntityFrameworkCore.Storage;

namespace DataBaseController.DAOs
{
    public class CustomReactionsDAO
    {
        public async Task Adicionar(CustomReactions cr)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                IDbContextTransaction transation = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("call AddCR({0}, {1}, {2}, {3})", cr.Trigger, cr.Resposta, cr.Modo, cr.Servidor.ID);
                await transation.CommitAsync();
            }
        }

        public async Task<CustomReactions> Get(CustomReactions cr)
        {
            using(Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.CustomReactions.FromSqlRaw("call CREvent({0}, {1})", cr.Servidor.ID, cr.Trigger).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task<CustomReactions[]> GetPage(CustomReactions cr, uint page)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.CustomReactions.FromSqlRaw("call Lcr({0}, {1}, {2})", cr.Servidor.ID, cr.Trigger, page).ToListAsync()).ToArray();
            }
        }
    }
}
