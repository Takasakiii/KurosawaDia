using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class JoinedGuildEvent
    {
        public async Task JoinedGuild(SocketGuild socketGuild)
        {
            await new DblExtensions().AtualizarDadosDbl();
        }
    }
}
