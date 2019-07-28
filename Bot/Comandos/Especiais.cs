using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;
using static Bot.DataBase.MainDB.Modelos.Servidores;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Especiais : Configuracoes
    {
        public void insult(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.permissoes == Permissoes.ServidorPika)
                    {
                        Insultos insulto = new Insultos();
                        if(new InsultosDAO().GetInsulto(ref insulto))
                        {
                            string[] comando = (string[])args[1];
                            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                            context.Message.DeleteAsync();
                            Tuple<IUser, string> user = new UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);
                            IUser insultAuthor = context.Client.GetUserAsync(insulto.usuario.id).GetAwaiter().GetResult();

                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription($"{user.Item1.Mention} {insulto.insulto}")
                                    .WithColor(Color.DarkPurple)
                                    .WithFooter(StringCatch.GetString("insultCriado", "Insulto criado por: {0}", insultAuthor.ToString()), insultAuthor.GetAvatarUrl())
                                .Build());
                        }
                    }
                }
            }
        }

        public void criarinsulto(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    if (servidor.permissoes == Permissoes.ServidorPika)
                    {
                        string[] comando = (string[])args[1];
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                        Usuarios usuario = new Usuarios(context.User.Id, context.User.ToString());

                        Insultos insulto = new Insultos();
                        insulto.SetInsulto(msg, usuario);

                        if(new InsultosDAO().InserirInsulto(insulto))
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithDescription(StringCatch.GetString("createinsultCriado", "**{0}** o insulto foi adicinado", context.User.ToString()))
                                    .WithColor(Color.DarkPurple)
                                .Build());
                        }
                    }
                }
            }
        }
    }
}
