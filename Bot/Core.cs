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
        DiscordSocketClient client;
        public async Task Async(string token, string prefix)
        {
            client = new DiscordSocketClient();
            new Nucleo.Eventos.MessageEvent(client, prefix);
            await client.LoginAsync(Discord.TokenType.Bot, token);
            await client.StartAsync();
            await Task.Delay(-1);
        }

        public async void DesligarAsync()
        {
            await client.StopAsync(); 
        }
        
        //***ULTRAGRAVE*** n preciso nem comentar q essa classe possui um erro muito erroneo de otimização relacionada ao DiscordSocketCliente || resolvida (eu acho)
    }
}
