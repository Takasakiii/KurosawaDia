using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using Discord.WebSocket;
using System.Threading.Tasks;

namespace Bot
{
    /*
     * Classe Core é responsavel por cadastrar os eventos na Discord.net e iniciar o bot
     */

    public class Core
    {
        //Metodo interno responsavel por definir as informações do clientes e seus eventos, alem de inserir o mesmo em seu singleton
        private void CriarCliente()
        {
            SingletonClient.criarClient();

            DiaConfig config = new DiaConfigDAO().Carregar();


            SingletonClient.client.MessageReceived += new MessageEvent(config, new ModulesConcat<GenericModule>()).MessageReceived;
            SingletonClient.client.LoggedIn += new LogInEvent(config).LogIn;
            SingletonClient.client.Log += new LogEvent().LogTask;
            SingletonClient.client.ShardReady += new ReadyEvent().ShardReady;
            SingletonClient.client.JoinedGuild += new JoinedGuildEvent().JoinedGuild;
            SingletonClient.client.LeftGuild += new LeftGuildEvent().LeftGuild;
            SingletonClient.client.UserJoined += new UserJoinedEvent().UserJoined;
            SingletonClient.client.UserLeft += new UserLeftEvent().UserLeft;

            Iniciar(config).GetAwaiter().GetResult();
        }

        //Tarefa interna responsavel por iniciar(logar) o bot
        private async Task Iniciar(DiaConfig diaConfig)
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

        //Metodo responsavel pelo inicio do bot
        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
