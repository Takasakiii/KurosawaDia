using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class JoinedGuildEvent
    {
        public Task JoinedGuild(SocketGuild socketGuild)
        {
            new DblExtensions().AtualizarDadosDbl();
            return Task.CompletedTask;
        }
    }
}
