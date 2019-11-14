using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class LeftGuildEvent
    {
        public async Task LeftGuild(SocketGuild socketGuild)
        {
            await new DblExtensions().AtualizarDadosDbl();
        }
    }
}
