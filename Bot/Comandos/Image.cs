using Bot.Constantes;
using Bot.Extensions;
using Bot.GenericTypes;
using Discord;
using Discord.Commands;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Linq;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Servidores;

namespace Bot.Comandos
{
    public class Image : GenericModule
    {
        public Image(CommandContext contexto, string prefixo, string[] comando) : base(contexto, prefixo, comando)
        {

        }

        public async Task cat()
        {
            Links links = new Links();
            await new ImageExtensions().getImg(Contexto, await StringCatch.GetStringAsync("catTxt", "Meow"), links.cat);
        }

        public async Task dog()
        {
            Links links = new Links();
            await new ImageExtensions().getImg(Contexto, img: links.dog);
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
                        embed.WithColor(Color.Red);
                        embed.WithDescription("");
                        embed.WithTitle(await StringCatch.GetStringAsync("magikavatarPessoa", "Eu não encontrei essa pessoa no servidor"));
                        embed.AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando: "), await StringCatch.GetStringAsync("usoMagikavatar", "`{0}magikavatar <pessoa>`", PrefixoServidor));
                        embed.AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo: "), await StringCatch.GetStringAsync("exemploMagikavatar", "`{0}magikavatar @KingCerverus#2490`", PrefixoServidor));
                    }
                }

                if (user != null)
                {
                    embed.WithDescription(await StringCatch.GetStringAsync("magikavatarAguarde", "**{0}** estou fazendo magica com o avatar por-favor aguarde", Contexto.User.ToString()));
                    embed.WithImageUrl(await StringCatch.GetStringAsync(" agikavatarAguardeImg", "https://i.imgur.com/EEKIQTv.gif"));
                    IUserMessage userMsg = Contexto.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                    try
                    {
                        string magikReturn = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={avatarUrl}&intensity=10", "message");

                        embed.WithImageUrl(magikReturn);
                        embed.WithDescription("");
                        await userMsg.DeleteAsync();
                    }
                    catch
                    {
                        await userMsg.DeleteAsync();
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("magikavatarErro", "**{0}** infelizmente a diretora mari roubou a minha magia", Contexto.User.ToString()));
                        embed.WithImageUrl(null);
                    }
                }
            }
            else
            {
                embed.WithColor(Color.Red);
                embed.WithDescription(await StringCatch.GetStringAsync("magikavatarDm", "Eu so posso pegar o avatar de outras pessoas em um servidor"));
            }

            await Contexto.Channel.SendMessageAsync(embed: embed.Build());

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
                        embed.WithDescription("");
                        embed.WithImageUrl(retorno);
                    }
                    catch
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("mgikErro", "**{0}** infelizmente a diretora mari roubou a minha magia", Contexto.User.ToString()));
                        embed.WithImageUrl(null);
                    }
                }
                else
                {
                    if (res.Item1 && res.Item2 < 102400) {
                        string retorno = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={imgUrl}&intensity=10", "message");
                        embed.WithDescription("");
                        embed.WithImageUrl(retorno);
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription(await StringCatch.GetStringAsync("mgiktamanho", "**{0}** sua imagem é muito poderosa para mim, por favor envie gifs até 100 kb 😥", Contexto.User.ToString()));
                        embed.WithImageUrl(null);
                    }
                }
                await userMsg.DeleteAsync();
            }
            else
            {
                embed.WithTitle(await StringCatch.GetStringAsync("magikSemImg", "Você precisa me falar qual imagem você quer que eu faça magica"));
                embed.AddField(await StringCatch.GetStringAsync("usoCmd", "Uso do Comando:"), await StringCatch.GetStringAsync("usoMagik", "`{0}magik <imagem>`", PrefixoServidor));
                embed.AddField(await StringCatch.GetStringAsync("exemploCmd", "Exemplo: "), await StringCatch.GetStringAsync("exemploMagik", "`{0}magik https://i.imgur.com/cZDlYXr.png`", PrefixoServidor));
                embed.WithColor(Color.Red);
            }

            await Contexto.Channel.SendMessageAsync(embed: embed.Build());
        }

        public async Task loli()
        {
            ulong id = 0;
            if (!Contexto.IsPrivate)
            {
                id = Contexto.Guild.Id;
            }

            Servidores servidor = new Servidores(id, PrefixoServidor.ToCharArray());

            Tuple<bool, Servidores> sucesso = await new ServidoresDAO().GetPermissoesAsync(servidor);
            if (!Contexto.IsPrivate && sucesso.Item1 && servidor.Permissoes == PermissoesServidores.ServidorPika || servidor.Permissoes == PermissoesServidores.LolisEdition || (await new AdmsExtensions().GetAdm(new Usuarios(Contexto.User.Id))).Item1)
            {
                Links links = new Links();
                await new ImageExtensions().getImg(Contexto, img: links.loli);
            }
            else
            {
                await new Ajuda(Contexto, PrefixoServidor, Comando).MessageEventExceptions(new NullReferenceException(), servidor);
            }

        }
    }
}
