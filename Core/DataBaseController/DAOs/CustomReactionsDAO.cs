using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public class CustomReactionsDAO
    {
        public async Task Adicionar(CustomReactions cr)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
                //await context.Database.ExecuteSqlRawAsync("call AddCR({0}, {1}, {2}, {3})", cr.Trigger, cr.Resposta, cr.Modo, cr.Servidor.ID);
                //_ = context.CustomReactions.FromSqlRaw("call AddCR({0}, {1}, {2}, {3})", cr.Trigger, cr.Resposta, cr.Modo, cr.Servidor.ID);
                //await transation.CommitAsync();

                MySqlCommand command = await context.GetMysqlCommand();
                command.CommandText = "call AddCR(@t, @r, @m, @si)";
                command.Parameters.AddWithValue("@t", cr.Trigger);
                command.Parameters.AddWithValue("@r", cr.Resposta);
                command.Parameters.AddWithValue("@m", cr.Modo);
                command.Parameters.AddWithValue("@si", cr.Servidor.ID);
                await command.ExecuteNonQueryAsync();
            }
        }

        public async Task<CustomReactions> Get(CustomReactions cr)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
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

        public async Task<int> Delete(CustomReactions cr)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                int res = 0;
                //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.ReadUncommitted);
                //res = await context.Database.ExecuteSqlRawAsync("call DeleteCR({0}, {1})", cr.Servidor.ID, cr.Cod);
                //await transation.CommitAsync();

                MySqlCommand command = await context.GetMysqlCommand();
                command.CommandText = "call DeleteCR(@si, @c)";
                command.Parameters.AddWithValue("@si", cr.Servidor.ID);
                command.Parameters.AddWithValue("@c", cr.Cod);
                res = await command.ExecuteNonQueryAsync();

                return res;
            }
        }
    }
}
