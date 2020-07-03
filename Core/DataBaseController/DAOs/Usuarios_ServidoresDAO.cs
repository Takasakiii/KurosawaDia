using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseController.DAOs
{
    public sealed class Usuarios_ServidoresDAO
    {
        public async Task<Servidores> Add(Servidores_Usuarios su)
        {
            Kurosawa_DiaContext context = new Kurosawa_DiaContext();

            Servidores servidor = await context.Servidores.AsNoTracking().SingleOrDefaultAsync(x => x.ID == su.Servidor.ID);

            if (servidor == null)
            {
                await context.Servidores.AddAsync(servidor = new Servidores
                {
                    ID = su.Servidor.ID,
                    Nome = su.Servidor.Nome
                });
                await context.SaveChangesAsync();
            }

            Usuarios usuario = await context.Usuarios.AsNoTracking().SingleOrDefaultAsync(x => x.ID == su.Usuario.ID);

            if (usuario == null)
            {
                await context.Usuarios.AddAsync(usuario = new Usuarios
                {
                    ID = su.Usuario.ID,
                    Nome = su.Usuario.Nome
                });
                await context.SaveChangesAsync();
            }

            Servidores_Usuarios servidor_usuario = await context.Servidores_Usuarios.AsNoTracking().SingleOrDefaultAsync(x => x.Servidor.ID == su.Servidor.ID && x.Usuario.ID == su.Usuario.ID);

            if (servidor_usuario == null)
            {
                await context.Servidores_Usuarios.AddAsync(servidor_usuario = new Servidores_Usuarios
                {
                    Servidor = servidor,
                    Usuario = usuario
                });
            }

            return servidor;

            //using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            //{
            //    return (await context.Servidores.FromSqlRaw("call CadastrarUsuarioServidor({0}, {1}, {2}, {3})", su.Servidor.ID, su.Usuario.ID, su.Servidor.Nome, su.Usuario.Nome).ToListAsync()).FirstOrDefault();
            //}
        }
    }
}


//IDbContextTransaction transation = await context.Database.BeginTransactionAsync(IsolationLevel.Snapshot);
//await context.Database.ExecuteSqlRawAsync("call CadastrarUsuarioServidor({0}, {1}, {2}, {3})", su.Servidor.ID, su.Usuario.ID, su.Servidor.Nome, su.Usuario.Nome);
//await transation.CommitAsync();
//context.AdmsBots.fro
//MySqlCommand cmd = await context.GetMysqlCommand();
//cmd.CommandText = "call CadastrarUsuarioServidor(@si, @ui, @sn, @un)";
//cmd.Parameters.AddWithValue("@si", su.Servidor.ID);
//cmd.Parameters.AddWithValue("@ui", su.Usuario.ID);
//cmd.Parameters.AddWithValue("@sn", su.Servidor.Nome);
//cmd.Parameters.AddWithValue("@un", su.Usuario.Nome);
//await cmd.ExecuteNonQueryAsync();