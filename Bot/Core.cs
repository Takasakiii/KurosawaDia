using System;
using System.Threading.Tasks;
using Bot.Modelos;
using Discord;
using Discord.WebSocket;

namespace Bot
{
    public class Core
    {
        private DiscordSocketClient client;

        private BotCore botCore = new BotCore(); 

        public void Iniciar(string token)
        {
            botCore.token = token;
            Async().GetAwaiter().GetResult();
        }

        private async Task Async()
        {
            client = new DiscordSocketClient();
            await client.LoginAsync(TokenType.Bot, botCore.token);
            await client.StartAsync();
            await Task.Delay(-1);
        }
    }
}
