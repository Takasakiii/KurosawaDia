using Bot.Modelos;
using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class MessageEvent
    {
        private readonly DiscordSocketClient client;
        private readonly AyuraConfig config;
        public MessageEvent(DiscordSocketClient client, AyuraConfig config)
        {
            this.client = client;
            this.config = config;
        }

        public async Task MessageReceived(SocketMessage socket)
        {
            SocketUserMessage msg = socket as SocketUserMessage;
            if (msg.Author.IsBot) return;
            if (msg == null) return;
            int argPos = 0;
            string[] comando;
            string tratada = "";
            CommandContext context = new CommandContext(client, msg);

            if(msg.HasStringPrefix(config.prefix, ref argPos))
            {
                tratada = context.Message.Content.Substring(config.prefix.Length).ToLower();
            }
            else if(msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                tratada = context.Message.Content.Substring(22).ToLower();
            }

            comando = tratada.Split(' ');
            await new Catalogo().IrComando(context, comando);
        }
    }
}
