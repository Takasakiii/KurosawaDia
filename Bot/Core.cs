﻿using System.Threading.Tasks;
using Bot.DAO;
using Bot.Modelos;
using Bot.Nucleo.Eventos;
using Discord.WebSocket;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            DiscordSocketClient client = new DiscordSocketClient();
            AyuraConfig config = new AyuraConfig(2);
            AyuraConfigDAO dao = new AyuraConfigDAO();
            config = dao.Carregar(config);

            client.MessageReceived += new MessageEvent(client, config).MessageReceived;

            Iniciar(client, config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(DiscordSocketClient client, AyuraConfig ayuraConfig)
        {
            await client.LoginAsync(Discord.TokenType.Bot, ayuraConfig.token);
            await client.StartAsync();
            await Task.Delay(-1);
        }
         
        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
