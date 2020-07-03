using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class FuckDAO
    {
        public async Task<Fuck> Get(Fuck f)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            if (f.Explicit)
            {
                Fuck[] fucks = await context.Fuck.ToArrayAsync();

                Random random = new Random();

                return fucks[random.Next(fucks.Length)];
            }
            else
            {
                Fuck[] fucks = await context.Fuck.Where(x => !x.Explicit).ToArrayAsync();

                Random random = new Random();

                return fucks[random.Next(fucks.Length)];
            }

            //return (await context.Fuck.FromSqlRaw("call GetFuck({0})", fuck.Explicit).ToListAsync()).FirstOrDefault();
        }

        public async Task Add(Fuck f)
        {
            using Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            Usuarios usuarios = await context.Usuarios.SingleOrDefaultAsync(x => x.ID == f.Usuario.ID);

            f.Usuario = usuarios;

            await context.Fuck.AddAsync(f);
            await context.SaveChangesAsync();

            //IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
            //await context.Database.ExecuteSqlRawAsync("call AddFuck({0}, {1}, {2})", fuck.Usuario.ID, fuck.Url, fuck.Explicit);
            //_ = context.Fuck.FromSqlRaw("call AddFuck({0}, {1}, {2})", fuck.Usuario.ID, fuck.Url, fuck.Explicit);
            //await transation.CommitAsync();

            //MySqlCommand command = await context.GetMysqlCommand();
            //command.CommandText = "call AddFuck(@ui, @u, @e)";
            //command.Parameters.AddWithValue("@ui", fuck.Usuario.ID);
            //command.Parameters.AddWithValue("@u", fuck.Url);
            //command.Parameters.AddWithValue("@e", fuck.Explicit);
            //await command.ExecuteNonQueryAsync();
        }
    }
}
