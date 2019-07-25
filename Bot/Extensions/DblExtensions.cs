using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using DiscordBotsList.Api;
using DiscordBotsList.Api.Objects;

namespace Bot.Extensions
{
    public class DblExtensions
    {
        public void AtualizarDadosDbl()
        {
            var apis = new ApisConfigDAO().Carregar();
            if (apis.Item1)
            {
                ApisConfig dbl = apis.Item2[1];
                if (dbl.Ativada)
                {
                    AuthDiscordBotListApi DblApi = new AuthDiscordBotListApi(SingletonClient.client.CurrentUser.Id, dbl.Token);
                    IDblSelfBot me = DblApi.GetMeAsync().GetAwaiter().GetResult();
                    me.UpdateStatsAsync(SingletonClient.client.Guilds.Count);
                }
            }
        }
    }
}
