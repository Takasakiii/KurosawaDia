using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;

namespace Bot.Comandos
{
//o jogo :D
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

        public void webcam(CommandContext context, object[] args)
        {
            SocketGuildUser usr = context.User as SocketGuildUser;

            if (!context.IsPrivate && usr.VoiceChannel != null)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithTitle($"Canal: {usr.VoiceChannel.Name}")
                        .WithDescription($"[clique aqui](https://discordapp.com/channels/{context.Guild.Id}/{usr.VoiceChannel.Id}) para poder compartilhar sua tela ou ligar sua webcam")
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

            var aa = Emote.Parse(comando[1]);
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

                    context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                            .WithDescription(msg)
                            .WithColor(Color.DarkPurple)
                        .Build());
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

        //nome bem top
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
    }
}
