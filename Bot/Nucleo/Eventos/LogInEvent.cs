using Bot.Extensions;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord.WebSocket;
using System;
using System.Threading;
using System.Threading.Tasks;
using static Bot.Extensions.StringVarsControler;

namespace Bot.Nucleo.Eventos
{
    public class LogInEvent
    {
        private readonly DiaConfig diaConfig = null;
        public LogInEvent(DiaConfig diaConfig)
        {
            this.diaConfig = diaConfig;
        }

        public Task LogIn()
        {
            new Thread(async () =>
            {
                Status[] status = await new StatusDAO().CarregarStatusAsync();
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
                            await LogEmiter.EnviarLogAsync(e);
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
