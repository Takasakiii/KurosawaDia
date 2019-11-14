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
                            int users = 0;
                            foreach (SocketGuild servidor in SingletonClient.client.Guilds)
                            {
                                users += servidor.Users.Count;
                            }

                            StringVarsControler varsControler = new StringVarsControler();
                            VarTypes[] vars = new VarTypes[] {
                                new VarTypes("%prefixo%", diaConfig.prefix),
                                new VarTypes("%servidores%", SingletonClient.client.Guilds.Count.ToString()),
                                new VarTypes("%usuarios%", users.ToString())
                            };
                            varsControler.AdicionarComplementos(vars);

                            switch (status[i].status_tipo)
                            {
                                case Status.TiposDeStatus.Jogando:
                                    await SingletonClient.client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Playing);
                                    break;
                                case Status.TiposDeStatus.Live:
                                    await SingletonClient.client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Streaming);
                                    break;
                                case Status.TiposDeStatus.Ouvindo:
                                    await SingletonClient.client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Listening);
                                    break;
                                case Status.TiposDeStatus.Assistindo:
                                    await SingletonClient.client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Watching);
                                    break;
                            }
                            varsControler = null;
                            GC.Collect();
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
