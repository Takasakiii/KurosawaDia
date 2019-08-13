using Discord;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Canais;

namespace Bot.Nucleo.Eventos
{
    public class UserJoinedEvent
    {
        public Task UserJoined(SocketGuildUser user)
        {
            Canais canal = new Canais(new Servidores(user.Guild.Id), TiposCanais.bemvindoCh);
            if (new CanaisDAO().GetCh(ref canal))
            {
                IMessageChannel channel = user.Guild.GetChannel(canal.Id) as IMessageChannel;
                channel.SendMessageAsync("yay");
            }

            return Task.CompletedTask;
        }
    }
}
