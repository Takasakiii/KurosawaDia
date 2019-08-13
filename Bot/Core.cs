using Bot.Nucleo;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using System.Threading.Tasks;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            SingletonClient.criarClient();

            DiaConfig config = new DiaConfigDAO().Carregar();

            SingletonClient.client.MessageReceived += new MessageEvent(config).MessageRecived;
            SingletonClient.client.LoggedIn += new LogInEvent().LogIn;
            SingletonClient.client.Log += new Log().LogTask;
            SingletonClient.client.Ready += new ReadyEvent().Ready;
            SingletonClient.client.JoinedGuild += new JoinedGuildEvent().JoinedGuild;
            SingletonClient.client.LeftGuild += new LeftGuildEvent().LeftGuild;
            SingletonClient.client.UserJoined += new UserJoinedEvent().UserJoined;
            //SingletonClient.client.UserLeft

            Iniciar(config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(DiaConfig diaConfig)
        {
            await SingletonClient.client.LoginAsync(Discord.TokenType.Bot, diaConfig.token);
            await SingletonClient.client.StartAsync();
            await Task.Delay(-1);
        }

        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
