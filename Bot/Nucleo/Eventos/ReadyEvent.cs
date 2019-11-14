using Bot.Extensions;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class ReadyEvent
    {
        public async Task ShardReady(DiscordSocketClient Cliente)
        {
            await new DblExtensions().AtualizarDadosDbl();
        }
    }
}
