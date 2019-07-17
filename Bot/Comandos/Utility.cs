using Bot.DataBase.MainDB.DAO;
using Bot.DataBase.MainDB.Modelos;
using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;

namespace Bot.Comandos
{
    //o jogo :D
    //eu vo mata the corno q pos isso aqui
    public class Utility : Weeb
    {
        public void avatar(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));


            if (!context.IsPrivate)
            {
                Tuple<IUser, string> getUser = new Extensions.UserExtensions().GetUser(context.Guild.GetUsersAsync().GetAwaiter().GetResult(), msg);
                if (getUser.Item1 != null || msg == "")
                {
                    IUser user;
                    if (msg != "")
                    {
                        user = getUser.Item1;
                    }
                    else
                    {
                        user = context.User;
                    }

                    string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithAuthor($"{user}")
                        .WithDescription($"[Link Direto]({avatarUrl})")
                        .WithImageUrl(avatarUrl)
                    .Build());
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** não encontrei essa pessoa")
                            .AddField("Uso do Comando: ", $"`{(string)args[0]}avatar @pessoa`")
                            .AddField("Exemplo: ", $"`{(string)args[0]}avatar @Hikari#3172`")
                            .WithColor(Color.Red)
                     .Build());
                }
            }
            else
            {
                if (msg == "")
                {
                    string avatar = context.User.GetAvatarUrl(0, 2048) ?? context.User.GetDefaultAvatarUrl();
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithAuthor($"{context.User}")
                        .WithDescription($"[Link Direto]({avatar})")
                        .WithImageUrl(avatar)
                    .Build());
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** desculpa mas eu não consigo pegar o avatar de outras pessoas no privado 😔")
                            .WithColor(Color.Red)
                     .Build());
                }
            }
        }

        public void videochamada(CommandContext context, object[] args)
        {
            SocketGuildUser usr = context.User as SocketGuildUser;

            if (!context.IsPrivate && usr.VoiceChannel != null)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription($"Para acessar o compartilhamento de tela basta [Clicar Aqui](https://discordapp.com/channels/{context.Guild.Id}/{usr.VoiceChannel.Id}) 😀")
                .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithDescription("você precisa estar em um canal de voz e em um servidor para usar esse comando")
                .Build());
            }
        }

        public void emote(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];

            EmbedBuilder embed = new EmbedBuilder();
            embed.WithColor(Color.DarkPurple);

            try
            {
                bool parse = Emote.TryParse(comando[1], out Emote emote);

                if (parse)
                {

                    embed.WithTitle(emote.Name);
                    embed.WithDescription($"[Link Direto]({emote.Url})");
                    embed.WithImageUrl(emote.Url);
                }
                else
                {
                    HttpExtensions http = new HttpExtensions();
                    string name = http.GetSiteHttp("https://ayura.com.br/links/emojis.json", comando[1]);

                    string shortName = "";
                    foreach (char character in name)
                    {
                        if (character != ':')
                        {
                            shortName += character;
                        }
                    }
                    string unicode = http.GetSite($"https://www.emojidex.com/api/v1/emoji/{shortName}", "unicode");

                    embed.WithTitle(shortName);
                    embed.WithDescription($"[Link Direto](https://twemoji.maxcdn.com/2/72x72/{unicode}.png)");
                    embed.WithImageUrl($"https://twemoji.maxcdn.com/2/72x72/{unicode}.png");
                }
            }
            catch
            {
                embed.WithDescription($"**{context.User}** o emoji que você tentou usar é inválido");
                embed.AddField("Uso do comando: ", $"`{(string)args[0]}emote emoji`");
                embed.AddField("Exemplo: ", $"`{(string)args[0]}emote :kanna:`");
                embed.WithColor(Color.Red);
            }

            context.Channel.SendMessageAsync(embed: embed.Build());
        }

        public void say(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    IGuildUser user = context.User as IGuildUser;
                    if (user.GuildPermissions.ManageMessages)
                    {
                        context.Message.DeleteAsync().GetAwaiter().GetResult();
                    }

                    new EmbedControl().SendMessage(context, msg);
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** você precisa de me falar uma mensagem")
                            .AddField("Uso do comando:", $"`{(string)args[0]}say <mensagem>`")
                            .AddField("Uso do comando:", $"`{(string)args[0]}say @Sora#5614 cade o wallpaper?`")
                            .WithColor(Color.Red)
                        .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("você so pode usar esse comando em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void simg(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                if (context.Guild.IconUrl != null)
                {
                    string url = $"{context.Guild.IconUrl}?size=2048";
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithTitle(context.Guild.Name)
                            .WithDescription($"[Link Direto]({url})")
                            .WithImageUrl(url)
                            .WithColor(Color.DarkPurple)
                        .Build());
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** o servidor não tem um icone")
                        .WithColor(Color.Red)
                    .Build());
                }
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** esse comando so pode ser usado em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void sugestao(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));
            string servidor = "";

            if (!context.IsPrivate)
            {
                servidor = context.Guild.Name;
            }
            else
            {
                servidor = "Privado";
            }

            if (msg != "")
            {
                IMessageChannel canal = context.Client.GetChannelAsync(556598669500088320).GetAwaiter().GetResult() as IMessageChannel;

                canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Nova sugestão de: {context.User}")
                        .AddField("Sugestão: ", msg)
                        .AddField("Servidor: ", servidor)
                        .WithColor(Color.DarkPurple)
                    .Build());

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** sua sugestão foi enviada a o meu servidor")
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** você precisa me falara uma sugestão")
                        .AddField("Uso: ", $"`{args[0]}sugestao <sugestão>`")
                        .AddField("Exemplo: ", $"`{args[0]}sugestao fazer com que o bot ficasse mais tempo on`")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void setprefix(CommandContext context, object[] args)
        {
            if (!context.IsPrivate)
            {
                string[] comando = (string[])args[1];
                string msg = string.Join(" ", comando, 1, (comando.Length - 1));

                if (msg != "")
                {
                    IUserMessage message = context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** você quer mudar o prefixo?")
                            .WithFooter("se não apenas ignore essa mensagem")
                            .WithColor(Color.DarkPurple)
                        .Build()).GetAwaiter().GetResult();

                    Emoji emoji = new Emoji("✅");
                    message.AddReactionAsync(emoji);

                    ReactionControler reaction = new ReactionControler();
                    reaction.GetReaction(message, emoji, context.User, new ReturnMethod((CommandContext contexto, object[] argumentos) => {
                        Servidores servidor = new Servidores(context.Guild.Id);
                        servidor.SetPrefix(msg.ToCharArray());

                        char[] prefix = new ServidoresDAO().SetServidorPrefix(servidor);

                        message.DeleteAsync();
                        context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                                .WithDescription($"**{context.User}** o prefixo do servidor foi alterado de: `{args[0]}` para: `{new string(prefix)}`")
                                .WithColor(Color.DarkPurple)
                            .Build());
                    }, context, args));
                }
                else
                {
                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription($"**{context.User}** você precisa me falar um prefixo")
                            .AddField("Uso: ", $"`{args[0]}setprefix <prefixo>`")
                            .AddField("Exemplo: ", $"`{args[0]}setprefix !`")
                            .WithColor(Color.Red)
                        .Build());
                }

            }
            else
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription("Esse comando so pode ser usado em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }
    }
}
