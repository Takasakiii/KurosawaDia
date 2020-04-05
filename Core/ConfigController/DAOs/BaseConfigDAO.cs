using ConfigController.EntityConfiguration;
using ConfigController.Models;
using System.Threading.Tasks;

namespace ConfigController.DAOs
{
    public sealed class BaseConfigDAO
    {
        public async Task Adicionar(BaseConfig config)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                config.Cod = 1;
                if (await contexto.BaseConfigs.FindAsync((uint)1) != null)
                {
                    contexto.BaseConfigs.Update(config);
                }
                else
                {
                    await contexto.BaseConfigs.AddAsync(config);
                }
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<BaseConfig> Ler()
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                return await contexto.BaseConfigs.FindAsync((uint)1);
            }
        }
    }
}
