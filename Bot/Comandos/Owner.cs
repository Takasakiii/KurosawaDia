using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using static Bot.DataBase.MainDB.Modelos.Adms;

namespace Bot.Comandos
{
    public class Owner : Nsfw
    {
        public void ping(CommandContext context, object[] args)
        {
            Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
            Adms adm = new Adms(usuario);

            if (new AdmsDAO().GetAdm(ref adm))
            {
                if (adm.permissoes == PermissoesAdm.Donas)
                {
                    DiscordSocketClient client = context.Client as DiscordSocketClient;

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription(StringCatch.GetString("respostaPing", "Meu ping é {0}", client.Latency)) //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                        .Build());
                }
            }
        }

        public void setespecial(CommandContext context, object[] args)
        {
            Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
            Adms adm = new Adms(usuario);

            if (new AdmsDAO().GetAdm(ref adm))
            {
                if (adm.permissoes == PermissoesAdm.Donas)
                {
                   
                }
            }
        }
    }
}
