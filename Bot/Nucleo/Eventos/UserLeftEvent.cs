using Bot.Extensions;
using Discord;
using Discord.WebSocket;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Canais;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace Bot.Nucleo.Eventos
{
    public class UserLeftEvent
    {
        public async Task UserLeft(SocketGuildUser user)
        {


            Canais canal = new Canais(new Servidores(user.Guild.Id), TiposCanais.sairCh);
            Tuple<bool, Canais> res = await new CanaisDAO().GetChAsync(canal);
            canal = res.Item2;
            if (res.Item1)
            {
                ConfiguracoesServidor configuracoes = new ConfiguracoesServidor(new Servidores(user.Guild.Id), new BemVindoGoodByeMsg());
                Tuple<bool, ConfiguracoesServidor> res2 = await new ConfiguracoesServidorDAO().GetByeMsgAsync(configuracoes);
                configuracoes = res2.Item2;
                if (res2.Item1)
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
