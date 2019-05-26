using Bot.DAO;
using Bot.Modelos;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            SingletonClient.criarClient();

            AyuraConfig config = new AyuraConfig(1);
            AyuraConfigDAO dao = new AyuraConfigDAO();
            config = dao.Carregar(config);

            SingletonClient.client.MessageReceived += new MessageEvent(SingletonClient.client, config).MessageRecived;

            Iniciar(config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(AyuraConfig ayuraConfig)
        {
            await SingletonClient.client.LoginAsync(Discord.TokenType.Bot, ayuraConfig.token);
            await SingletonClient.client.StartAsync();
            await SingletonClient.client.SetGameAsync("Flores");
            await Task.Delay(-1);
        }

        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
