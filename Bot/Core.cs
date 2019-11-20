using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord.WebSocket;
using System;
using System.Threading.Tasks;

namespace Bot
{


    public class Core
    {
        public async Task CriarClienteAsync()
        {
            bool repetir = false;
            do
            {
                try
                {
                    SingletonClient.criarClient();

                    DiaConfig config = await new DiaConfigDAO().CarregarAsync();


                    SingletonClient.client.MessageReceived += new MessageEvent(config, new ModulesConcat<GenericModule>()).MessageReceived;
                    SingletonClient.client.LoggedIn += new LogInEvent(config).LogIn;
                    SingletonClient.client.Log += new LogEvent().LogTask;
                    SingletonClient.client.ShardReady += new ReadyEvent().ShardReady;
                    SingletonClient.client.JoinedGuild += new JoinedGuildEvent().JoinedGuild;
                    SingletonClient.client.LeftGuild += new LeftGuildEvent().LeftGuild;
                    SingletonClient.client.UserJoined += new UserJoinedEvent().UserJoined;
                    SingletonClient.client.UserLeft += new UserLeftEvent().UserLeft;

                    await IniciarAsync(config);
                    repetir = false;
                }
                catch (Exception e)
                {
                    repetir = true;
                    await LogEmiter.EnviarLogAsync(e);
                }
            } while (repetir);
        }

        private async Task IniciarAsync(DiaConfig diaConfig)
        {
            await SingletonClient.client.LoginAsync(Discord.TokenType.Bot, diaConfig.token);
            await SingletonClient.client.StartAsync();
            string shardsIDs = "";
            foreach (DiscordSocketClient socket in SingletonClient.client.Shards)
            {
                shardsIDs += $" {socket.ShardId} ";
            }
            await LogEmiter.EnviarLogAsync(LogEmiter.TipoLog.TipoCor.Debug, $"Shards[{SingletonClient.client.Shards.Count.ToString()}]: {shardsIDs}");
            await Task.Delay(-1);
        }


    }
}
