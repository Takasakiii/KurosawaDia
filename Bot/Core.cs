using Bot.Nucleo;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using ConfigurationControler.ConfigDB.DAO;
using ConfigurationControler.ConfigDB.Modelos;
using System.Threading.Tasks;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            SingletonClient.criarClient();

            AyuraConfig config = new AyuraConfig(1);
            AyuraConfigDAO dao = new AyuraConfigDAO();
            config = dao.Carregar(config);

            SingletonClient.client.MessageReceived += new MessageEvent(config).MessageRecived;
            SingletonClient.client.LoggedIn += new LogInEvent().LogIn;
            SingletonClient.client.Log += new Log().LogTask;

            Iniciar(config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(AyuraConfig ayuraConfig)
        {
            await SingletonClient.client.LoginAsync(Discord.TokenType.Bot, ayuraConfig.token);
            await SingletonClient.client.StartAsync();
            await Task.Delay(-1);
        }

        public void IniciarBot()
        {
            CriarCliente();
        }
    }
}
