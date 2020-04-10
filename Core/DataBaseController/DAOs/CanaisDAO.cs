using DataBaseController.Contexts;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
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
    }
}
