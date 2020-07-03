using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public class AdmsBotDAO
    {
        public async Task<AdmsBot> Get(AdmsBot adms)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            return await context.AdmsBots.AsNoTracking().SingleOrDefaultAsync(x => x.Usuario.ID == adms.Usuario.ID);

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    return (await context.AdmsBots.FromSqlRaw("call LerAdms({0})", adms.Usuario.ID).ToListAsync()).FirstOrDefault();
            //}
        }

        public async Task Atualizar(AdmsBot adms)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            AdmsBot admsBot = await context.AdmsBots.SingleOrDefaultAsync(x => x.Usuario.ID == adms.Usuario.ID);

            if (admsBot != null)
            {
                admsBot.Permissao = adms.Permissao;
                await context.SaveChangesAsync();
            }
            else
            {
                await context.AdmsBots.AddAsync(new AdmsBot
                {
                    Usuario = new Usuarios
                    {
                        Cod = (await context.Usuarios.SingleOrDefaultAsync(x => x.ID == adms.Usuario.ID)).Cod
                    },
                    Permissao = adms.Permissao
                });
            }

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            //    //await context.Database.ExecuteSqlRawAsync("call AtualizarAdm({0}, {1})", adms.Usuario.ID, adms.Permissao);
            //    //_ = context.AdmsBots.FromSqlRaw("call AtualizarAdm({0}, {1})", adms.Usuario.ID, adms.Permissao);
            //    //await transation.CommitAsync();

            //    MySqlCommand command = await context.GetMysqlCommand();
            //    command.CommandText = "call AtualizarAdm(@ui, @up)";
            //    command.Parameters.AddWithValue("@ui", adms.Usuario.ID);
            //    command.Parameters.AddWithValue("@up", adms.Permissao);
            //    await command.ExecuteNonQueryAsync();
            //}
        }
    }
}
