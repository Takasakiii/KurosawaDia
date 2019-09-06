using Bot.Extensions;
using Discord;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Nucleo.Eventos
{
    public class UserJoinedEvent
    {
        public Task UserJoined(SocketGuildUser user)
        {
            Canais canal = new Canais(new Servidores(user.Guild.Id), TiposCanais.bemvindoCh);
            if (new CanaisDAO().GetCh(ref canal))
            {
                ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(user.Guild.Id), new BemVindoGoodByeMsg());
                if (new ConfiguracoesServidorDAO().GetWelcomeMsg(ref configuracoes))
                {
                    IMessageChannel channel = user.Guild.GetChannel(canal.Id) as IMessageChannel;
                    StringVarsControler varsControler = new StringVarsControler(user: user);
                    new EmbedControl().SendMessage(channel, varsControler.SubstituirVariaveis(configuracoes.bemvindo.bemvindoMsg));
                }
            }

            return Task.CompletedTask;
        }
    }
}
