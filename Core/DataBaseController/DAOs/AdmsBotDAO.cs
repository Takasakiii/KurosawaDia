using DataBaseController.Contexts;
using DataBaseController.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

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
    }
}
