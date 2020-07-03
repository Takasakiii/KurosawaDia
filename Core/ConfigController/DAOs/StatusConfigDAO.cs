using ConfigController.EntityConfiguration;
using ConfigController.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ConfigController.DAOs
{
    public sealed class StatusConfigDAO
    {
        public async Task Adicionar(StatusConfig[] statusConfig)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                await contexto.StatusConfig.AddRangeAsync(statusConfig);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<StatusConfig[]> Ler()
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                return (await contexto.StatusConfig.ToListAsync()).ToArray();
            }
        }

        public async Task Deletar(StatusConfig statusConfig)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                contexto.StatusConfig.Remove(statusConfig);
                await contexto.SaveChangesAsync();
            }
        }
    }
}
