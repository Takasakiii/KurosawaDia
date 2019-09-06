using Bot.Extensions;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord.WebSocket;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

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
                Status[] status = new StatusDAO().CarregarStatus().Item2.ToArray();
                do
                {
                    for (int i = 0; i < status.Length; i++)
                    {
                        try
                        {
                            StringVarsControler varsControler = new StringVarsControler(null);
                            varsControler.AdicionarComplemento(new StringVarsControler.VarTypes("%prefixo%", diaConfig.prefix));
                            varsControler.AdicionarComplemento(new StringVarsControler.VarTypes("%servidores%", SingletonClient.Client.Guilds.Count.ToString()));

                            int users = 0;
                            foreach (SocketGuild servidor in SingletonClient.Client.Guilds)
                            {
                                users += servidor.Users.Count;
                            }

                            varsControler.AdicionarComplemento(new StringVarsControler.VarTypes("%usuarios%", users.ToString()));

                            switch (status[i].status_tipo)
                            {
                                case Status.TiposDeStatus.Jogando:
                                    await SingletonClient.Client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Playing);
                                    break;
                                case Status.TiposDeStatus.Live:
                                    await SingletonClient.Client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Streaming);
                                    break;
                                case Status.TiposDeStatus.Ouvindo:
                                    await SingletonClient.Client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Listening);
                                    break;
                                case Status.TiposDeStatus.Assistindo:
                                    await SingletonClient.Client.SetGameAsync(varsControler.SubstituirVariaveis(status[i].status_jogo), status[i].status_url, Discord.ActivityType.Watching);
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
