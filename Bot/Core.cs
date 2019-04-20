using Bot.DAO;
using Bot.Modelos;
using Bot.Nucleo.Eventos;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            AyuraConfig config = new AyuraConfig(1);
            AyuraConfigDAO dao = new AyuraConfigDAO();
            config = dao.Carregar(config);

            client.MessageReceived += new MessageEvent(client, config).MessageRecived;

            Iniciar(client, config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(DiscordSocketClient client, AyuraConfig ayuraConfig)
        {
            await client.LoginAsync(Discord.TokenType.Bot, ayuraConfig.token);
            await client.StartAsync();
            await client.SetGameAsync("Flores");
            await Task.Delay(-1);
        }

        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
