using ConfigController.EntityConfiguration;
using ConfigController.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConfigController.DAOs
{
    public class DBConfigDAO
    {
        public async Task Adicionar(DBConfig config)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                config.Cod = 1;
                if (await contexto.DBConfig.AsNoTracking().FirstOrDefaultAsync(x => x.Cod == 1) != null)
                {
                    contexto.DBConfig.Update(config);
                }
                else
                {
                    await contexto.DBConfig.AddAsync(config);
                }
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<DBConfig> Ler()
        {
            using (KurosawaConfigContext contexo = new KurosawaConfigContext())
            {
                return await contexo.DBConfig.FindAsync((ushort)1);
            }
        }
    }
}
