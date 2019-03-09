using System.Threading.Tasks;
using Bot.Modelos;
using Discord.WebSocket;

namespace Bot
{
    public class Core
    {
        DiscordSocketClient client;
        public async Task Async(Tokens tk)
        {
            client = new DiscordSocketClient();
            new Nucleo.Eventos.MessageEvent(client, tk.prefix);
            await client.LoginAsync(Discord.TokenType.Bot, tk.botToken);
            await client.StartAsync();
            await client.SetGameAsync("Flores");
            await Task.Delay(-1);
        }

        public async void DesligarAsync()
        {
            await client.StopAsync(); 
        }
    }
}
