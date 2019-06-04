using Bot.Singletons;
using Discord;
using System.Reflection;
using System.Threading.Tasks;

namespace Bot.Nucleo
{
    public class Log
    {
        public Task LogTask(LogMessage msg)
        {
            MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
            object[] parms = new object[1];
            parms[0] = msg.ToString();
            metodo.Invoke(SingletonLogs.instanced, parms);

            return Task.CompletedTask;
        }
    }
}
