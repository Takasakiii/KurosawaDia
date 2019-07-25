using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using static Bot.DataBase.MainDB.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Owner : Nsfw
    {
        public void ping(CommandContext context, object[] args)
        {
            DiscordSocketClient client = context.Client as DiscordSocketClient;

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription(StringCatch.GetString("respostaPing", "Meu ping é {0}", client.Latency)) //pedreragem top e continua aki em av3 kkkkkkkk esperando esse comentario em av4 kkkkkkk
                .Build());
        }

        //public void teste(CommandContext context, object[] args)
        //{
        //    Servidores servidor = new Servidores(context.Guild.Id);
        //    string perms = "";
        //    if (new ServidoresDAO().GetPermissoes(ref servidor))
        //    {
        //        switch (servidor.permissoes)
        //        {
        //            case Permissoes.Normal:
        //                perms = "normal";
        //                break;
        //            case Permissoes.ServidorBot:
        //                perms = "servidor com pika de 30km";
        //                break;
        //            case Permissoes.LolisEdition:
        //                perms = "olha os hentai bando de fudido";
        //                break;
        //        }
        //        context.Channel.SendMessageAsync(perms);
        //    }
        //}
    }
}
