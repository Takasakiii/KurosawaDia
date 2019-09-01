using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
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
                Status[] status = new StatusDAO().CarregarStatus().Item2.ToArray();
                do
                {
                    for (int i = 0; i < status.Length; i++)
                    {
                        try
                        {
                            switch (status[i].status_tipo)
                            {
                                case Status.TiposDeStatus.Jogando:
                                    await SingletonClient.client.SetGameAsync(status[i].status_jogo, status[i].status_url, Discord.ActivityType.Playing);
                                    break;
                                case Status.TiposDeStatus.Live:
                                    await SingletonClient.client.SetGameAsync(status[i].status_jogo, status[i].status_url, Discord.ActivityType.Streaming);
                                    break;
                                case Status.TiposDeStatus.Ouvindo:
                                    await SingletonClient.client.SetGameAsync(status[i].status_jogo, status[i].status_url, Discord.ActivityType.Listening);
                                    break;
                                case Status.TiposDeStatus.Assistindo:
                                    await SingletonClient.client.SetGameAsync(status[i].status_jogo, status[i].status_url, Discord.ActivityType.Watching);
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
                        finally
                        {
                            Thread.Sleep(8000);
                        }
                    }
                } while (status.Length != 0);
            }).Start();
            return Task.CompletedTask;
        }
    }
}
