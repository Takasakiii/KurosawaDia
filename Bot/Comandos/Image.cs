using Bot.Constantes;
using Bot.Extensions;
using Bot.GenericTypes;
using Bot.Modelos;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Servidores;
using static Bot.Extensions.ErrorExtension;

namespace Bot.Comandos
{
    public class Image : GenericModule
    {
        public bool Autorizado = false;
        public Image(CommandContext contexto, params object[] args) : base(contexto, args)
        {
            VerificarOwner().Wait();
        }

        private async Task VerificarOwner()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Tuple<bool, Servidores> servidor = await new ServidoresDAO().GetPermissoesAsync(new Servidores(id));
            if (servidor.Item2.Permissoes == PermissoesServidores.LolisEdition || servidor.Item2.Permissoes == PermissoesServidores.ServidorPika || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                Autorizado = true;
            }

        }

        public async Task cat()
        {
            Links links = new Links();
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Texto: await StringCatch.GetStringAsync("catTxt", "Meow")), links.cat);
        }

        public async Task dog()
        {
            Links links = new Links();
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel), links.dog);
        }

        public async Task magikavatar()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            if (!Contexto.IsPrivate)
            {
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg);
                IUser user = null;

                if (getUser.Item1 != null)
                {
                    user = getUser.Item1;
                }
                else
                {
                    if (msg == "")
                    {
                        user = Contexto.User;
                    }
                    else
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("magikavatarPessoa", "eu não encontrei essa pessoa no servidor."), new DadosErro(await StringCatch.GetStringAsync("parametroPessoa", "@pessoa"), "@KingCerverus#2490"));
                    }
                }

                if (user != null)
                {
                    embed.WithDescription(await StringCatch.GetStringAsync("magikavatarAguarde", "**{0}**, estou fazendo mágica com o avatar. Por favor, aguarde.", Contexto.User.ToString()));
                    embed.WithImageUrl(await StringCatch.GetStringAsync("magikavatarAguardeImg", "https://i.imgur.com/EEKIQTv.gif"));
                    IUserMessage userMsg = Contexto.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                    try
                    {
                        string magikReturn = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={avatarUrl}&intensity=10", "message");
                        embed.WithImageUrl(magikReturn);
                        embed.WithDescription("");
                        await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                    }
                    catch
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("magikavatarErro", "infelizmente a diretora Mari roubou a minha magia 😔"));
                    }
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("magikavatarDm", "eu só posso pegar o avatar de outras pessoas em um servidor."));
            }
        }

        public async Task magik()
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            string[] comando = Comando;
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string imgUrl = "";

            if (msg != "")
            {
                imgUrl = msg;
            }
            else if (Contexto.Message.Attachments.Count != 0)
            {
                imgUrl = Contexto.Message.Attachments.First().Url;
            }

            if (imgUrl != "")
            {
                embed.WithDescription(await StringCatch.GetStringAsync("magikAguarde", "**{0}** estou fazendo magica com a imagem por-favor aguarde", Contexto.User.ToString()));
                embed.WithImageUrl(await StringCatch.GetStringAsync("magikAguardeImg", "https://i.imgur.com/EEKIQTv.gif"));
                IUserMessage userMsg = Contexto.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                Tuple<bool, long> res = await new HttpExtensions().PegarTamanhoArquivo(imgUrl);

                if (!imgUrl.Contains("gif"))
                {
                    try
                    {
                        string retorno = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={imgUrl}&intensity=10", "message");
                        embed.WithImageUrl(retorno);
                        embed.WithDescription("");
                        await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                    }
                    catch
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("mgikErro", "infelizmente a diretora mari roubou a minha magia 😔"));
                    }
                }
                else
                {
                    if (res.Item1 && res.Item2 < 102400) {
                        string retorno = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={imgUrl}&intensity=10", "message");
                        embed.WithImageUrl(retorno);
                        embed.WithDescription("");
                        await Contexto.Channel.SendMessageAsync(embed: embed.Build());
                    }
                    else
                    {
                        await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("mgiktamanho", "sua imagem é muito poderosa para mim. Por favor, envie GIFs até 100kb 😥"));
                    }
                }
            }
            else
            {
                await Erro.EnviarErroAsync(await StringCatch.GetStringAsync("magikSemImg", "bocê precisa me falar com que imagem você quer que eu faça mágica."), new DadosErro(await StringCatch.GetStringAsync("usoImagem", "<imagem>"), "https://i.imgur.com/cZDlYXr.png"));
            }
        }

        public async Task loli()
        {
            if (Autorizado)
            {
                Links links = new Links();
                await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel), links.loli);
            }
            else
            {
                throw new NullReferenceException();
            }

        }

        public async Task lolibomb()
        {
            if (Autorizado)
            {
                Links links = new Links();
                await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Quantidade: 5), links.loli);
            }
            else
            {
                throw new NullReferenceException();
            }
        }
    }
}
