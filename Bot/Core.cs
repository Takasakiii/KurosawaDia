using Bot.DAO;
using Bot.Modelos;
using Bot.Nucleo.Eventos;
using Bot.Singletons;
using Discord;
using System.Reflection;
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

            SingletonClient.client.MessageReceived += new MessageEvent(SingletonClient.client, config).MessageRecived;
            SingletonClient.client.LoggedIn += new LogInEvent(SingletonClient.client).LogIn;
            SingletonClient.client.Log += Log;

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

        private Task Log(LogMessage msg)
        {
            MethodInfo metodo = SingletonErros.tipo.GetMethod("Log");
            object[] parms = new object[1];
            parms[0] = msg.ToString();
            metodo.Invoke(SingletonErros.instanced, parms);

            return Task.CompletedTask;
        }
    }
}
