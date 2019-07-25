using Bot.Extensions;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class ReadyEvent
    {
        public Task Ready()
        {
            new DblExtensions().AtualizarDadosDbl();
            return Task.CompletedTask;
        }
    }
}
