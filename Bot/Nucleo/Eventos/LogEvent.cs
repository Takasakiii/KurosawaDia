using Bot.Extensions;
using Bot.Singletons;
using Discord;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    //Classe responsavel por tratar o evento de log da discord.net para a interface de usuario
    public class LogEvent
    {
        //Evento que captura o log da discord.net e joga para a interface de usuario
        public async Task LogTask(LogMessage msg)
        {
            await LogEmiter.EnviarLogAsync(LogEmiter.TipoLog.TipoCor.Info, msg.ToString()) ;
        }
    }
}
