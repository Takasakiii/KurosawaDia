using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using DiscordBotsList.Api;
using DiscordBotsList.Api.Objects;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class DblExtensions
    {
        public async Task AtualizarDadosDbl()
        {
            ApisConfig[] apis = await new ApisConfigDAO().CarregarAsync();
            if (apis.Length > 0)
            {
                ApisConfig dbl = apis[1];
                if (dbl.Ativada)
                {
                    AuthDiscordBotListApi DblApi = new AuthDiscordBotListApi(SingletonClient.client.CurrentUser.Id, dbl.Token);
                    IDblSelfBot me = DblApi.GetMeAsync().GetAwaiter().GetResult();
                    await me.UpdateStatsAsync(SingletonClient.client.Guilds.Count);
                }
            }
        }
    }
}
