using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Threading.Tasks;
using static Bot.Extensions.ErrorExtension;
using static MainDatabaseControler.Modelos.Servidores;
using UserExtensions = Bot.Extensions.UserExtensions;

namespace Bot.Comandos
{
    public class Especiais : GenericModule
    {
        private bool Autorizado = false;
        public Especiais(CommandContext contexto, params object[] args) : base(contexto, args)
        {
            VerificarServidorUsuario().Wait();
        }

        private async Task VerificarServidorUsuario()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            if((await new ServidoresDAO().GetPermissoesAsync(new Servidores(id))).Item2.Permissoes == PermissoesServidores.ServidorPika || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                Autorizado = true;
            }

        }

        public async Task insult()
        {
            if (Autorizado)
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
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("insultSemPessoa", "você não me disse quem deve insultar.", new DadosErro(await StringCatch.GetStringAsync("usoPessoa", "@pessoa"), "@Brunoow#7239")));
                    }
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("insultFail", "infelizmente ainda não tenho nenhum insulto 😔"));
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }

        public async Task criarinsulto()
        {
            if (Autorizado)
            {
                string[] comando = Comando;
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    Usuarios usuario = new Usuarios(Contexto.User.Id, Contexto.User.ToString());
                    Insultos insulto = new Insultos(msg, usuario);
                    if (await new InsultosDAO().InserirInsultoAsync(insulto))
                    {
                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("createinsultCriado", "**{0}**, o insulto foi adicionado.", Contexto.User.ToString()))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                }
                else
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("criarinsultoErro", "Você precisa me falar um insulto."), new DadosErro("insulto", "joguei uma pedra em você e ela entrou em órbita"));
                }
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public async Task fuckadd()
        {
            if (Autorizado)
            {
                string[] comando = Comando;
                try
                {
                    if (await new HttpExtensions().IsImageUrl(comando[1]))
                    {
                        bool _explicit = Convert.ToBoolean(comando[2]);
                        Usuarios usuario = new Usuarios(Contexto.User.Id, Contexto.User.ToString());
                        Fuck fuck = new Fuck(_explicit, comando[1], usuario);
                        await new FuckDAO().AddImgAsync(fuck);

                        await Contexto.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription(await StringCatch.GetStringAsync("addFuckCriado", "**{0}**, a imagem foi adicionada.", Contexto.User.ToString()))
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }
                    else
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("addFucknEhImg", "isso não é uma imagem."));
                    }
                }
                catch
                {
                    await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("fuckAddErro", "você precisa fornecer a url da imagem e se ela é explicita ou não."), new DadosErro("<Url Img> <Explicit>", "https://i.imgur.com/JDlJzBC.gif false"));
                }
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}