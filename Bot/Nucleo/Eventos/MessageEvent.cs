using Bot.Modelos;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo.Eventos
{
    public class MessageEvent
    {
        private DiscordSocketClient client;
        string prefix = "";
        private WeebClient weebClient;

        public MessageEvent(DiscordSocketClient client, string prefix, WeebClient weebClient)
        {
            client.MessageReceived += MessageRecived;
            this.client = client;
            this.prefix = prefix;
            this.weebClient = weebClient;
        }

        public async Task MessageRecived(SocketMessage socket)
        {
            var msg = socket as SocketUserMessage;
            if (msg == null) return;
            int argPos = 0;
            string[] comando;
            string tratada = "";
            var context = new CommandContext(client, msg);
            string prefix = this.prefix;

            if(msg.HasStringPrefix(prefix, ref argPos))
            {
                tratada = context.Message.Content.Substring(prefix.Length);
            }
            else if(msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                tratada = context.Message.Content.Substring(22);
            }

            comando = tratada.Split(' ');
            await new Catalogo().IrComando(context, client, socket, comando, weebClient);
        }
    }
}
