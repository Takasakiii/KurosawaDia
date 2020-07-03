using DataBaseController.Contexts;
using DataBaseController.Factory;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public class CanaisDAO
    {
        public async Task<Canais> Get(Canais canal)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            return await context.Canais.SingleOrDefaultAsync(x => x.Servidor.ID == canal.Servidor.ID && x.TipoCanal == canal.TipoCanal);

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    return (await context.Canais.FromSqlRaw("call GetCanal({0}, {1})", canal.Servidor.ID, canal.TipoCanal).ToListAsync()).FirstOrDefault();
            //}
        }

        public async Task Adicionar(Canais c)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            Canais canal = await context.Canais.SingleOrDefaultAsync(x => x.Servidor.ID == c.Servidor.ID && x.TipoCanal == c.TipoCanal);

            if (canal == null)
            {
                await context.Canais.AddAsync(c);
                await context.SaveChangesAsync();
            }
            else
            {
                context.Canais.Remove(canal);
                await context.SaveChangesAsync();
            }

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            //    //await context.Database.ExecuteSqlRawAsync("call AddCanal({0}, {1}, {2}, {3})", canal.TipoCanal, canal.Nome, canal.ID, canal.Servidor.ID);
            //    //_ = context.Canais.FromSqlRaw("call AddCanal({0}, {1}, {2}, {3})", canal.TipoCanal, canal.Nome, canal.ID, canal.Servidor.ID);
            //    //await transation.CommitAsync();

            //    MySqlCommand command = await context.GetMysqlCommand();
            //    command.CommandText = "call AddCanal(@ct, @cn, @ci, @csi)";
            //    command.Parameters.AddWithValue("@ct", canal.TipoCanal);
            //    command.Parameters.AddWithValue("@cn", canal.Nome);
            //    command.Parameters.AddWithValue("@ci", canal.ID);
            //    command.Parameters.AddWithValue("@csi", canal.Servidor.ID);
            //    await command.ExecuteNonQueryAsync();
            //}
        }
    }
}
