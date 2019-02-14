using System;
using System.Threading.Tasks;
using Bot.Modelos;
using Discord;
using Discord.WebSocket;
using Weeb.net;

namespace Bot
{
    public class Core
    {
        private DiscordSocketClient client;

        private BotCore botCore = new BotCore();

        WeebClient weebClient = new WeebClient("Yummi", "1.0.0");

        public void Iniciar(string token, string prefix, string weebToken)
        {
            botCore.token = token;
            botCore.prefix = prefix;
            botCore.weebToken = weebToken;
            Async().GetAwaiter().GetResult();
        }

        private async Task Async()
        {
            client = new DiscordSocketClient();
            new Nucleo.Eventos.MessageEvent(client, botCore.prefix, weebClient);
            await client.LoginAsync(Discord.TokenType.Bot, botCore.token);
            await weebClient.Authenticate(botCore.weebToken, Weeb.net.TokenType.Wolke);
            await client.StartAsync();
            await Task.Delay(-1);
        }
    }
}
