using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public class AdmsBotDAO
    {
        public async Task<AdmsBot> Get(AdmsBot adms)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.AdmsBots.FromSqlRaw("call LerAdms({0})", adms.Usuario.ID).ToListAsync()).FirstOrDefault();
            }
        }

        public async Task Atualizar(AdmsBot adms)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                IDbContextTransaction transaction = await context.Database.BeginTransactionAsync();
                await context.Database.ExecuteSqlRawAsync("call AtualizarAdm({0}, {1})", adms.Usuario.ID, adms.Permissao);
                await transaction.CommitAsync();
            }
        }
    }
}
