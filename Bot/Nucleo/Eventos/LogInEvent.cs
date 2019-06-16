using Bot.DataBase.ConfigDB.DAO;
using Bot.DataBase.ConfigDB.Modelos;
using Bot.Singletons;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class LogInEvent
    {
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
                                    await SingletonClient.client.SetGameAsync(status[i].status, type: Discord.ActivityType.Playing);
                                    break;
                                case 1:
                                    await SingletonClient.client.SetGameAsync(status[i].status, type: Discord.ActivityType.Streaming);
                                    break;
                                case 2:
                                    await SingletonClient.client.SetGameAsync(status[i].status, type: Discord.ActivityType.Listening);
                                    break;
                                case 3:
                                    await SingletonClient.client.SetGameAsync(status[i].status, type: Discord.ActivityType.Watching);
                                    break;
                            }
                        }
                        catch (Exception e)
                        {
                            MethodInfo metodo = SingletonLogs.tipo.GetMethod("Log");
                            object[] parms = new object[1];
                            parms[0] = e.ToString();
                            metodo.Invoke(SingletonLogs.instanced, parms);
                        }
                        Thread.Sleep(8000);
                    }
                } while (true);
            }).Start();

            return Task.CompletedTask;
        }
    }
}
