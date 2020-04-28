using ConfigController.EntityConfiguration;
using ConfigController.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigController.DAOs
{
    public sealed class ApiConfigDAO
    {
        public async Task Adicionar(ApiConfig[] apiconfig)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                await contexto.ApiConfig.AddRangeAsync(apiconfig);
                await contexto.SaveChangesAsync();
            }
        }

        public async Task<ApiConfig[]> Ler()
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                return (await contexto.ApiConfig.ToListAsync()).ToArray();
            }
        }

        public async Task Deletar(ApiConfig apiConfig)
        {
            using (KurosawaConfigContext contexto = new KurosawaConfigContext())
            {
                contexto.ApiConfig.Remove(apiConfig);
                await contexto.SaveChangesAsync();
            }
        }
    }
}