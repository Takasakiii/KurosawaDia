using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class LeftGuildEvent
    {
        public Task LeftGuild(SocketGuild socketGuild)
        {
            new DblExtensions().AtualizarDadosDbl();
            return Task.CompletedTask;
        }
    }
}
