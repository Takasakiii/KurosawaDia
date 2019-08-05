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
                    Usuarios usuario = new Usuarios(context.User.Id, context.User.Username);
                    Adms adm = new Adms(usuario);
                    bool certo = new AdmsDAO().GetAdm(ref adm);
                    if (servidor.permissoes == Permissoes.ServidorPika || (adm.permissoes == Adms.PermissoesAdm.Donas && certo))
                    {
                        Insultos insulto = new Insultos();
                        if (new InsultosDAO().GetInsulto(ref insulto))
                        {
                            string[] comando = (string[])args[1];
                            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                            Tuple<IUser, string> user = new UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);

                            if (user.Item1 != null)
                            {
                                context.Message.DeleteAsync();
                                string author = "";
                                string icon = "";
                                try
                                {
                                    IUser authorUser = context.Client.GetUserAsync(insulto.usuario.id).GetAwaiter().GetResult();
                                    author = authorUser.ToString();
                                    icon = authorUser.GetAvatarUrl();
                                }
                                catch
                                {
                                    author = insulto.usuario.nome;
                                }

                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription($"{user.Item1.Mention} {insulto.insulto}")
                                        .WithColor(Color.DarkPurple)
                                        .WithFooter(StringCatch.GetString("insultCriado", "Insulto criado por: {0}", author), icon)
                                    .Build());
                            }
                            else
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithTitle(StringCatch.GetString("insultSemPessoa", "Você não me disse quem deve insultar"))
                                        .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoInsult", "`{0}insult @pessoa`", (string)args[0]))
                                        .AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("usoInsult", "`{0}insult @Brunoow#7239`", (string)args[0]))
                                        .WithColor(Color.Red)
                                    .Build());
                            }
                        }
                    }
                    else
                    {
                        new Ajuda().MessageEventExceptions(new NullReferenceException(), context, servidor);
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
                    Usuarios usuario = new Usuarios(context.User.Id, context.User.ToString());
                    Adms adm = new Adms(usuario);
                    bool certo = new AdmsDAO().GetAdm(ref adm);

                    if (servidor.permissoes == Permissoes.ServidorPika || (adm.permissoes == Adms.PermissoesAdm.Donas && certo))
                    {
                        string[] comando = (string[])args[1];
                        string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                        if (msg != "")
                        {
                            Insultos insulto = new Insultos();
                            insulto.SetInsulto(msg, usuario);

                            if (new InsultosDAO().InserirInsulto(insulto))
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("createinsultCriado", "**{0}** o insulto foi adicinado", context.User.ToString()))
                                        .WithColor(Color.DarkPurple)
                                    .Build());
                            }
                        }
                        else
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(StringCatch.GetString("criarinsultoErro", "Você precisa me falar um insulto"))
                                .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoCriarinsulto", "`{0}criarinsulto insulto`", (string)args[0]))
                                .AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("exemploCirarinsulto", "`{0}criarinsulto joguei uma pedra em você e ela entrou em orbita`", (string)args[0]))
                                .WithColor(Color.Red)
                               .Build());
                        }
                    }
                    else
                    {
                        new Ajuda().MessageEventExceptions(new NullReferenceException(), context, servidor);
                    }
                }
            }
        }

        public void fuckadd(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                Servidores servidor = new Servidores(context.Guild.Id);
                if (new ServidoresDAO().GetPermissoes(ref servidor))
                {
                    Usuarios usuario = new Usuarios(context.User.Id, context.User.ToString());
                    Adms adm = new Adms(usuario);
                    bool certo = new AdmsDAO().GetAdm(ref adm);

                    if (servidor.permissoes == Permissoes.ServidorPika || (adm.permissoes == Adms.PermissoesAdm.Donas && certo))
                    {
                        string[] comando = (string[])args[1];
                        try
                        {
                            if (new HttpExtensions().IsImageUrl(comando[1]))
                            {
                                bool _explicit = Convert.ToBoolean(comando[2]);
                                Fuck fuck = new Fuck();
                                fuck.SetImg(_explicit, comando[1], usuario);
                                new FuckDAO().AddImg(fuck);

                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("addFuckCriado", "**{0}** a imagem foi adicionada", context.User.ToString()))
                                        .WithColor(Color.DarkPurple)
                                    .Build());
                            }
                            else
                            {
                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription(StringCatch.GetString("addFucknEhImg", "**{0}** isso n eh uma imagem meu caro", context.User.ToString()))
                                        .WithColor(Color.Red)
                                    .Build());
                            }

                        }
                        catch
                        {
                            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(StringCatch.GetString("fuckAddErro", "{0} Eh meu caro teve um erro na hora de adicionar a img", context.User.ToString()))
                                    .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoFuckAdd", "`{0}fuckadd <Url Img> <Explicit>`", (string)args[0]))
                                    .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("usoFuckAdd", "`{0}fuckadd https://i.imgur.com/JDlJzBC.gif false`", (string)args[0]))
                                    .WithColor(Color.Red)
                                .Build());
                        }

                    }
                }
            }
        }


    }
}
