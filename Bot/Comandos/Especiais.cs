using Bot.Extensions;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using static MainDatabaseControler.Modelos.Servidores;
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
                    if (servidor.Permissoes == PermissoesServidores.ServidorPika || new AdmsExtensions().GetAdm(new Usuarios(context.User.Id)).Item1)
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
                                    IUser authorUser = context.Client.GetUserAsync(insulto.Usuario.Id).GetAwaiter().GetResult();
                                    author = authorUser.ToString();
                                    icon = authorUser.GetAvatarUrl();
                                }
                                catch
                                {
                                    author = insulto.Usuario.Nome;
                                }

                                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithDescription($"{user.Item1.Mention} {insulto.Insulto}")
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
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                if (!cmdContext.IsPrivate)
                {
                    Servidores servidor = new Servidores(context.Guild.Id);
                    if (new ServidoresDAO().GetPermissoes(ref servidor))
                    {
                        Usuarios usuario = new Usuarios(context.User.Id, context.User.ToString());
                        if (servidor.Permissoes == PermissoesServidores.ServidorPika || new AdmsExtensions().GetAdm(usuario).Item1)
                        {
                            string[] comando = (string[])cmdArgs[1];
                            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                            if (msg != "")
                            {
                                Insultos insulto = new Insultos(msg, usuario);
                                if (new InsultosDAO().InserirInsulto(insulto))
                                {
                                    cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription(StringCatch.GetString("createinsultCriado", "**{0}** o insulto foi adicinado", cmdContext.User.ToString()))
                                            .WithColor(Color.DarkPurple)
                                        .Build());
                                }
                            }
                            else
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                    .WithTitle(StringCatch.GetString("criarinsultoErro", "Você precisa me falar um insulto"))
                                    .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoCriarinsulto", "`{0}criarinsulto insulto`", (string)cmdArgs[0]))
                                    .AddField(StringCatch.GetString("exemploCmd", "Exemplo:"), StringCatch.GetString("exemploCirarinsulto", "`{0}criarinsulto joguei uma pedra em você e ela entrou em orbita`", (string)cmdArgs[0]))
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
            }, context, args).EsperarOkDb();
        }

        public void fuckadd(CommandContext context, object[] args)
        {
            new BotCadastro((CommandContext cmdContext, object[] cmdArgs) =>
            {
                if (!cmdContext.IsPrivate)
                {
                    Servidores servidor = new Servidores(cmdContext.Guild.Id);
                    if (new ServidoresDAO().GetPermissoes(ref servidor))
                    {
                        Usuarios usuario = new Usuarios(cmdContext.User.Id, cmdContext.User.ToString());

                        if (servidor.Permissoes == PermissoesServidores.ServidorPika || new AdmsExtensions().GetAdm(usuario).Item1)
                        {
                            string[] comando = (string[])cmdArgs[1];
                            try
                            {
                                if (new HttpExtensions().IsImageUrl(comando[1]))
                                {
                                    bool _explicit = Convert.ToBoolean(comando[2]);
                                    Fuck fuck = new Fuck(_explicit, comando[1], usuario);
                                    new FuckDAO().AddImg(fuck);

                                    cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription(StringCatch.GetString("addFuckCriado", "**{0}** a imagem foi adicionada", cmdContext.User.ToString()))
                                            .WithColor(Color.DarkPurple)
                                        .Build());
                                }
                                else
                                {
                                    cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                            .WithDescription(StringCatch.GetString("addFucknEhImg", "**{0}** isso n eh uma imagem meu caro", cmdContext.User.ToString()))
                                            .WithColor(Color.Red)
                                        .Build());
                                }

                            }
                            catch
                            {
                                cmdContext.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                        .WithTitle(StringCatch.GetString("fuckAddErro", "{0} Eh meu caro teve um erro na hora de adicionar a img", cmdContext.User.ToString()))
                                        .AddField(StringCatch.GetString("usoCmd", "Uso do Comando:"), StringCatch.GetString("usoFuckAdd", "`{0}fuckadd <Url Img> <Explicit>`", (string)cmdArgs[0]))
                                        .AddField(StringCatch.GetString("exemploCmd", "Exemplo: "), StringCatch.GetString("usoFuckAdd", "`{0}fuckadd https://i.imgur.com/JDlJzBC.gif false`", (string)cmdArgs[0]))
                                        .WithColor(Color.Red)
                                    .Build());
                            }

                        }
                    }
                }
            }, context, args).EsperarOkDb();
        }


    }
}
