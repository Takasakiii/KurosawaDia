using Discord.Commands;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class MessageEvent
    {
        private DiscordSocketClient client; // n eh dependencia de todos os metodos
        string prefix = ""; //se esse tado tem uma classe relacionada pq ele esta souto??? (¯\_(ツ)_/¯)(¯\_(ツ)_/¯)
        public MessageEvent(DiscordSocketClient client, string prefix)
        {
            client.MessageReceived += MessageRecived;
            this.client = client;
            this.prefix = prefix;
        }

        public async Task MessageRecived(SocketMessage socket)
        {
            SocketUserMessage msg = socket as SocketUserMessage;
            if (msg == null) return;
            int argPos = 0;
            string[] comando;
            string tratada = "";
            CommandContext context = new CommandContext(client, msg);

            if(msg.HasStringPrefix(prefix, ref argPos))
            {
                tratada = context.Message.Content.Substring(prefix.Length).ToLower();
            }
            else if(msg.HasMentionPrefix(client.CurrentUser, ref argPos))
            {
                tratada = context.Message.Content.Substring(22).ToLower();
            }

            comando = tratada.Split(' ');
            await new Catalogo().IrComando(context, comando); //Client participa de context então chamada n eh valida, obs 2: segundo que poderia ter uma sobrecarga pra quando nem todos
            //os parametros estejam em uso
        }
    }
}
