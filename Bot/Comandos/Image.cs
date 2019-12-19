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
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel, Texto: "Meow"), links.cat);
        }

        public async Task dog()
        {
            Links links = new Links();
            await new ImageExtensions().GetImgAsync(new ImgModel(Contexto.Channel), links.dog);
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
                IUser user = new Extensions.UserExtensions().GetUser(await Contexto.Guild.GetUsersAsync(), msg).Item1;

                if (user != null)
                {
                    imgUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();
                }
                else
                {
                    imgUrl = msg;
                }
            }
            else if (Contexto.Message.Attachments.Count != 0)
            {
                imgUrl = Contexto.Message.Attachments.First().Url;
            }

            if (imgUrl != "" && await new HttpExtensions().IsImageUrl(imgUrl))
            {
                embed.WithDescription($"**{Contexto.User}**, estou fazendo magica com a imagem. Por favor, aguarde.")
                .WithImageUrl("https://i.imgur.com/EEKIQTv.gif");
                IUserMessage userMsg = Contexto.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();
                Tuple<bool, long> res = await new HttpExtensions().PegarTamanhoArquivo(imgUrl);
                embed.WithDescription(string.Empty);
                string retorno;

                try
                {
                    if (imgUrl.Contains(".gif") && !res.Item1 && res.Item2 > 102400)
                    {
                        await userMsg.DeleteAsync();
                        await Erro.EnviarErroAsync("sua imagem é muito poderosa para mim. Por favor, envie GIFs até 100kb 😥");
                        return;
                    }
                    retorno = await new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={imgUrl}&intensity=10", "message");
                }
                catch
                {
                    await userMsg.DeleteAsync();
                    await Erro.EnviarErroAsync("infelizmente a diretora Mari roubou a minha magia 😔");
                    return;
                }

                embed.WithImageUrl(retorno);
                await userMsg.DeleteAsync();
                await Contexto.Channel.SendMessageAsync(embed: embed.Build());
            }
            else
            {
                await Erro.EnviarErroAsync("você precisa me falar com que imagem você quer que eu faça mágica.", new DadosErro("<imagem>", "https://i.imgur.com/cZDlYXr.png"), new DadosErro("<usuario", "@Kurosawa Dia#2439"));
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
