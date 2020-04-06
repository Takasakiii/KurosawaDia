using ConfigController.EntityConfiguration;
using ConfigController.Models;
using System.Threading.Tasks;

namespace ConfigController.DAOs
{
    public class DBConfigDAO
    {
        public async Task Adicionar(DBConfig config)
        {
            config.Cod = 1;
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                if (!(await contexto.DBConfig.FindAsync((ushort)config.Cod) != null))
                {
                    await contexto.DBConfig.AddAsync(config);
                }
                else
                {
                    contexto.DBConfig.Update(config);
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
