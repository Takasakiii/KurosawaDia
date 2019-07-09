using Bot.DataBase.ConfigDB.DAO;
using Bot.DataBase.ConfigDB.Modelos;
using Bot.Nucleo;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using System.Threading.Tasks;

namespace Bot
{
    public class Core
    {
        private void CriarCliente()
        {
            SingletonClient.criarClient();

            DiaConfig config = new DiaConfig(1);
            DiaConfigDAO dao = new DiaConfigDAO();
            config = dao.Carregar(config);

            SingletonClient.client.MessageReceived += new MessageEvent(config).MessageRecived;
            SingletonClient.client.LoggedIn += new LogInEvent().LogIn;
            SingletonClient.client.Log += new Log().LogTask;

            Iniciar(config).GetAwaiter().GetResult();
        }

        private async Task Iniciar(DiaConfig ayuraConfig)
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
