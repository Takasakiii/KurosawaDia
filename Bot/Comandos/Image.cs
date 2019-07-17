using Bot.Constantes;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using System;
using System.Linq;
using System.Threading;

namespace Bot.Comandos
{
    public class Image : Moderacao
    {
        public Links links = new Links();
        public void getImg(CommandContext context, string txt = "", Tuple<string, string> img = null, Tuple<string, string>[] imgs = null, bool nsfw = false, int quantidade = 1)
        {
            new Thread(() =>
            {
                if (imgs == null)
                {
                    Tuple<string, string>[] tuple =
                    {
                        img
                    };

                    imgs = tuple;
                }

                Random rand = new Random();
                int i = rand.Next(imgs.Length);

                HttpExtensions http = new HttpExtensions();

                EmbedBuilder embed = new EmbedBuilder();
                embed.WithColor(Color.DarkPurple);
                embed.WithImageUrl(http.GetSite(imgs[i].Item1, imgs[i].Item2));
                embed.WithTitle(txt);

                if (!nsfw)
                {
                    context.Channel.SendMessageAsync(embed: embed.Build());
                }
                else
                {
                    ITextChannel canal = context.Channel as ITextChannel;
                    if (context.IsPrivate || canal.IsNsfw)
                    {
                        if (quantidade <= 1)
                        {
                            context.Channel.SendMessageAsync(embed: embed.Build());
                        }
                        else
                        {
                            for (int x = 0; x < quantidade; x++)
                            {
                                int y = rand.Next(imgs.Length);
                                embed.WithImageUrl(http.GetSite(imgs[i].Item1, imgs[i].Item2));
                                context.Channel.SendMessageAsync(embed: embed.Build());
                            }
                        }
                    }
                    else
                    {
                        embed.WithImageUrl(null);
                        embed.WithColor(Color.Red);
                        embed.WithDescription($"**{context.User}** esse comando só pode ser usado em canais NSFW");
                        context.Channel.SendMessageAsync(embed: embed.Build());
                    }
                }
            }).Start();
        }

        public void neko(CommandContext context, object[] args)
        {
            getImg(context, "Um pouco de meninas gato (ou gatos com skin) sempre faz bem", links.neko);
        }

        public void cat(CommandContext context, object[] args)
        {
            getImg(context, "Meow", links.cat);
        }

        public void dog(CommandContext context, object[] args)
        {
            getImg(context, "Meow", links.dog);
        }

        public void img(CommandContext context, object[] args)
        {
            getImg(context, "Uma simples imagem pra usar onde quiser", links.img);
        }

        public void magikavatar(CommandContext context, object[] args)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            if (!context.IsPrivate)
            {
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);
                IUser user = null;

                if(getUser.Item1 != null)
                {
                    user = getUser.Item1;
                }
                else
                {
                    if(msg == "")
                    {
                        user = context.User;
                    }
                    else
                    {
                        embed.WithColor(Color.Red);
                        embed.WithDescription("");
                        embed.WithTitle("Eu não encontrei essa pessoa no servidor");
                        embed.AddField("Uso do Comando: ", $"`{args[0]}magikavatar <pessoa>`");
                        embed.AddField("Exemplo: ", $"`{args[0]}magikavatar @KingCerverus#2490`");
                    }
                }

                if(user != null)
                {
                    embed.WithDescription($"**{context.User}** estou fazendo magica com o avatar por-favor aguarde");
                    embed.WithImageUrl("https://i.imgur.com/EEKIQTv.gif");
                    IUserMessage userMsg = context.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                    try
                    {
                        string magikReturn = new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={avatarUrl}&intensity=10", "message");

                        embed.WithImageUrl(magikReturn);
                        embed.WithDescription("");
                        userMsg.DeleteAsync();
                    }
                    catch
                    {
                        userMsg.DeleteAsync();
                        embed.WithColor(Color.Red);
                        embed.WithDescription($"**{context.User}** infelizmente a diretora mari roubou a minha magia");
                        embed.WithImageUrl(null);
                    }
                }
            }
            else
            {
                embed.WithColor(Color.Red);
                embed.WithDescription("Eu so posso pegar o avatar de outras pessoas em um servidor");
            }

            context.Channel.SendMessageAsync(embed: embed.Build());

        }

        public void magik(CommandContext context, object[] args)
        {
            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string imgUrl = "";

            if(msg != "")
            {
                imgUrl = msg;
            }
            else
            {
                imgUrl = context.Message.Attachments.First().Url;
            }

            if(imgUrl != "")
            {
                embed.WithDescription($"**{context.User}** estou fazendo magica com a imagem por-favor aguarde");
                embed.WithImageUrl("https://i.imgur.com/EEKIQTv.gif");
                IUserMessage userMsg = context.Channel.SendMessageAsync(embed: embed.Build()).GetAwaiter().GetResult();

                try
                {
                    string retorno = new HttpExtensions().GetSite($"https://nekobot.xyz/api/imagegen?type=magik&image={imgUrl}&intensity=10", "message");
                    userMsg.DeleteAsync();
                    embed.WithDescription("");
                    embed.WithImageUrl(retorno);
                }
                catch
                {
                    userMsg.DeleteAsync();
                    embed.WithColor(Color.Red);
                    embed.WithDescription($"**{context.User}** infelizmente a diretora mari roubou a minha magia");
                    embed.WithImageUrl(null);
                }
            }
            else
            {
                embed.WithTitle("Você precisa me falar qual imagem você quer que eu faça magica");
                embed.AddField("Uso do Comando:", $"`{(string)args[0]}magik <imagem>`");
                embed.AddField("Exemplo: ", $"`{(string)args[0]}magik https://i.imgur.com/cZDlYXr.png`");
                embed.WithColor(Color.Red);
            }
            context.Channel.SendMessageAsync(embed: embed.Build());
        }
    }
}
