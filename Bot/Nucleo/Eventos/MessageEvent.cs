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
        string prefix = ""; //se esse tado tem uma classe relacionada pq ele esta souto??? (¯\_(ツ)_/¯)

        public MessageEvent(DiscordSocketClient client, string prefix)
        {
            client.MessageReceived += MessageRecived;
            this.client = client;
            this.prefix = prefix;
        }

        public async Task MessageRecived(SocketMessage socket)
        {
            var msg = socket as SocketUserMessage;
            if (msg == null) return;
            int argPos = 0;
            string[] comando;
            string tratada = "";
            var context = new CommandContext(client, msg);
            //alem de ser uma variavel replicada desnecessaria como disse acima esse dado devia estar vinculado com seu propria classe || Resolvido

            if(msg.HasStringPrefix(prefix, ref argPos))
            {
                tratada = context.Message.Content.Substring(prefix.Length).ToLower();
            }
            else if(msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                tratada = context.Message.Content.Substring(22).ToLower();
            }

            comando = tratada.Split(' ');
            await new Catalogo().IrComando(context, client, socket, comando); //vc ta puxando um objeto inteiro entre varias classes q n a usam para entregar a um destinatario longe, de vez de criar ele num lugar ja util || resolvido (eu acho)
        }
    }
}
