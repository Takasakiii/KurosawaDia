using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
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


            SingletonClient.Client.MessageReceived += new MessageEvent(config, new ModulesConcat<GenericModule>()).MessageReceived;
            SingletonClient.Client.LoggedIn += new LogInEvent(config).LogIn;
            SingletonClient.Client.Log += new LogEvent().LogTask;
            SingletonClient.Client.Ready += new ReadyEvent().Ready;
            SingletonClient.Client.JoinedGuild += new JoinedGuildEvent().JoinedGuild;
            SingletonClient.Client.LeftGuild += new LeftGuildEvent().LeftGuild;
            SingletonClient.Client.UserJoined += new UserJoinedEvent().UserJoined;
            SingletonClient.Client.UserLeft += new UserLeftEvent().UserLeft;

            Iniciar(config).GetAwaiter().GetResult();
        }

        //Tarefa interna responsavel por iniciar(logar) o bot
        private async Task Iniciar(DiaConfig diaConfig)
        {
            await SingletonClient.Client.LoginAsync(Discord.TokenType.Bot, diaConfig.token);
            await SingletonClient.Client.StartAsync();
            await Task.Delay(-1);
        }

        //Metodo responsavel pelo inicio do bot
        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
