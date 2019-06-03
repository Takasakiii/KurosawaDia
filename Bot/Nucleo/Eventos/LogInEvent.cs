using Bot.Configs.DAO;
using Bot.Configs.Modelos;
using Bot.Singletons;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class LogInEvent
    {
        private readonly DiscordSocketClient client;
        public LogInEvent(DiscordSocketClient client)
        {
            this.client = client;
        }

        public Task LogIn()
        {
            new Thread(async () =>
            {
                List<StatusConfig> status = new StatusDAO().getStatus();
                do
                {
                    for (int i = 0; i < status.Count; i++)
                    {
                        try
                        {
                            switch (status[i].tipo)
                            {
                                case 0:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Playing);
                                    break;
                                case 1:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Streaming);
                                    break;
                                case 2:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Listening);
                                    break;
                                case 3:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Watching);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            MethodInfo metodo = SingletonErros.tipo.GetMethod("Log");
                            object[] parms = new object[1];
                            parms[0] = e.ToString();
                            metodo.Invoke(SingletonErros.instanced, parms);
                        }
                        Thread.Sleep(8000);
                    }
                } while (true);
            }).Start();

            return Task.CompletedTask;
        }
    }
}
