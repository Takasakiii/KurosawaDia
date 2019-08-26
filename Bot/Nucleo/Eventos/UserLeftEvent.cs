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
    public class UserLeftEvent
    {
        public Task UserLeft(SocketGuildUser user)
        {


            Canais canal = new Canais(new Servidores(user.Guild.Id), TiposCanais.sairCh);
            if (new CanaisDAO().GetCh(ref canal))
            {
                ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(user.Guild.Id), new BemVindoGoodByeMsg());
                if (new ConfiguracoesServidorDAO().GetByeMsg(ref configuracoes))
                {
                    IMessageChannel channel = user.Guild.GetChannel(canal.Id) as IMessageChannel;
                    StringVarsControler varsControler = new StringVarsControler(user: user);
                    new EmbedControl().SendMessage(channel, varsControler.SubstituirVariaveis(configuracoes.bemvindo.sairMsg));
                }
            }
            return Task.CompletedTask;
        }
    }
}
