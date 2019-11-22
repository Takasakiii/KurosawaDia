using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Servidores;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Especiais : GenericModule
    {

        public Especiais(CommandContext contexto, params object[] args) : base(contexto, args)
        {

        }

        public async Task insult()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Servidores servidor = new Servidores(id, PrefixoServidor.ToCharArray());

            Tuple<bool, Servidores> sucesso = await new ServidoresDAO().GetPermissoesAsync(servidor);
            if (!Contexto.IsPrivate && sucesso.Item1 && servidor.Permissoes == PermissoesServidores.ServidorPika || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                Insultos insulto = new Insultos();
                Tuple<bool, Insultos> res = await new InsultosDAO().GetInsultoAsync(insulto);
                insulto = res.Item2;
                if (res.Item1)
                {
                    string[] comando = Comando;
                    string msg = string.Join(" ", comando, 1, (comando.Length - 1));
                    Tuple<IUser, string> user = new UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg);

                    if (user.Item1 != null)
                    {
                        await Contexto.Message.DeleteAsync();
                        string author = "";
                        string icon = "";
                        try
                        {
                            IUser authorUser = await Contexto.Client.GetUserAsync(insulto.Usuario.Id);
                            author = authorUser.ToString();
                            icon = authorUser.GetAvatarUrl();
                        }
                        catch
                        {
                            author = insulto.Usuario.Nome;
                        }

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"{user.Item1.Mention} {insulto.Insulto}")
                                .WithColor(Color.DarkPurple)
                                .WithFooter(await StringCatch.GetStringAsync("insultCriado", "Insulto criado por: {0}", author), icon)
                            .Build());
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithTitle(await StringCatch.GetStringAsync("insultSemPessoa", "Você não me disse quem deve insultar"))
                                .AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoInsult", "`{0}insult @pessoa`", PrefixoServidor))
                                .AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo:"), await StringCatch.GetStringAsync("usoInsult", "`{0}insult @Brunoow#7239`", PrefixoServidor))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(await StringCatch.GetStringAsync("insultFail", "**{0}** infelizmente ainda não tem nenhum insulto 😔", Contexto.User.Username))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await new Ajuda(Contexto, PrefixoServidor, Comando, Erro).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }

        public async Task criarinsulto()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Servidores servidor = new Servidores(id, PrefixoServidor.ToCharArray());
            Usuarios usuario = new Usuarios(Contexto.User.Id, Contexto.User.ToString());

            Tuple<bool, Servidores> sucesso = await new ServidoresDAO().GetPermissoesAsync(servidor);
            if (!Contexto.IsPrivate && sucesso.Item1 && servidor.Permissoes == PermissoesServidores.ServidorPika || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                string[] comando = Comando;
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    Insultos insulto = new Insultos(msg, usuario);
                    if (await new InsultosDAO().InserirInsultoAsync(insulto))
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("createinsultCriado", "**{0}** o insulto foi adicinado", Contexto.User.ToString()))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                }
                else
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle(await StringCatch.GetStringAsync("criarinsultoErro", "Você precisa me falar um insulto"))
                        .AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoCriarinsulto", "`{0}criarinsulto insulto`", PrefixoServidor))
                        .AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo:"), await StringCatch.GetStringAsync("exemploCirarinsulto", "`{0}criarinsulto joguei uma pedra em você e ela entrou em orbita`", PrefixoServidor))
                        .WithColor(Color.Red)
                       .Build());
                }
            }
            else
            {
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }

        }

        public async Task fuckadd()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Servidores servidor = new Servidores(id, PrefixoServidor.ToCharArray());
            Usuarios usuario = new Usuarios(Contexto.User.Id, Contexto.User.ToString());

            Tuple<bool, Servidores> sucesso = await new ServidoresDAO().GetPermissoesAsync(servidor);
            if (!Contexto.IsPrivate && sucesso.Item1 && servidor.Permissoes == PermissoesServidores.ServidorPika || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                string[] comando = Comando;
                try
                {
                    if (await new HttpExtensions().IsImageUrl(comando[1]))
                    {
                        bool _explicit = Convert.ToBoolean(comando[2]);
                        Fuck fuck = new Fuck(_explicit, comando[1], usuario);
                        await new FuckDAO().AddImgAsync(fuck);

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("addFuckCriado", "**{0}** a imagem foi adicionada", Contexto.User.ToString()))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("addFucknEhImg", "**{0}** isso n eh uma imagem meu caro", Contexto.User.ToString()))
                                .WithColor(Color.Red)
                            .Build());
                    }
                }
                catch
                {
                    await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(await StringCatch.GetStringAsync("fuckAddErro", "{0} você precisa fornecer a url da imagem e se ela é explicita ou não", Contexto.User.ToString()))
                            .AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoFuckAdd", "`{0}fuckadd <Url Img> <Explicit>`", PrefixoServidor))
                            .AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo: "), await StringCatch.GetStringAsync("usoFuckAdd", "`{0}fuckadd https://i.imgur.com/JDlJzBC.gif false`", PrefixoServidor))
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }
        }
    }
}