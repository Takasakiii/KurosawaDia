using DataBaseController.Contexts;
using DataBaseController.Modelos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataBaseController.DAOs
{
    public sealed class ServidoresDAO
    {
        public async Task<Servidores> Get(Servidores servidor)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                return (await context.Servidores.FromSqlRaw($"call GetServidor({servidor.ID})").ToListAsync()).FirstOrDefault();
            }
        }
    }
}
