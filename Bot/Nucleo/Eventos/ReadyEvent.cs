using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class ReadyEvent
    {
        public Task ShardReady(DiscordSocketClient Cliente)
        {
            new DblExtensions().AtualizarDadosDbl();
            return Task.CompletedTask;
        }
    }
}
