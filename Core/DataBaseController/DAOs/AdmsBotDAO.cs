using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Data;
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
                IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted);
                await context.Database.ExecuteSqlRawAsync("call AtualizarAdm({0}, {1})", adms.Usuario.ID, adms.Permissao);
                await transation.CommitAsync();
            }
        }
    }
}
