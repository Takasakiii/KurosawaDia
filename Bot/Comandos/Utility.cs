using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.Webhook;
using Discord.WebSocket;
using System;

namespace Bot.Comandos
{
    public class Utility : Owner
    {
        public void avatar(CommandContext context, object[] args)
        {
            Tuple<bool, IUser> getUser = new User().GetUserAsync(context, args);
            IUser user;
            if(getUser.Item1)
            {
                user = getUser.Item2;
            } else
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
            try
            {
                Emote emote = Emote.Parse(comando[1]);
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                         .WithTitle(emote.Name)
                         .WithUrl(emote.Url)
                         .WithImageUrl(emote.Url)
                         .WithColor(Color.DarkPurple)
                     .Build());
            }
            catch (ArgumentException)
            {

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** desculpe mas o meu dono eh um baiano e ainda n consegue aumentar os emoji padrão do discord")
                        .WithColor(Color.DarkPurple)
                    .Build());
            }
            catch (Exception e)
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithDescription($"**{context.User}** o emoji que você tentou usar é inválido {e}")
                    .AddField("Uso do comando: ", $"`{(string)args[0]}emote emoji`")
                    .AddField("Exemplo: ", $"`{(string)args[0]}emote :kanna:`")
                    .WithColor(Color.Red)
                .Build());
            }

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
                            .AddField("Uso do comando:", $"`{(string)args[0]}say @Thhrag#2527 sai do facebook`")
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
                        .WithDescription($"**{context.User}** esse comando so pode ser usado em servidores")
                        .WithColor(Color.Red)
                    .Build());
            }
        }

        public void sugestao(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            if (msg != "")
            {
                IMessageChannel canal = context.Client.GetChannelAsync(556598669500088320).GetAwaiter().GetResult() as IMessageChannel;

                canal.SendMessageAsync(embed: new EmbedBuilder()
                        .WithTitle($"Nova sugestão de: {context.User}")
                        .AddField("Sugestão: ", msg)
                        .AddField("Servidor: ", context.Guild.Name)
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

        public void fakemsg(CommandContext context, object[] args)
        {
            string[] comando = (string[])args[1];
            string msg = string.Join(" ", comando, 1, (comando.Length - 1));

            context.Message.DeleteAsync().GetAwaiter().GetResult();
            SocketTextChannel textChannel = context.Channel as SocketTextChannel;

            Tuple<bool, IUser> getUser = new User().GetUserAsync(context, args);
            if(getUser.Item1)
            {
                SocketGuildUser user = getUser.Item2 as SocketGuildUser;
                string nome = "";
                string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

                if (user.Nickname != null)
                {
                    nome = user.Nickname;
                }
                else
                {
                    nome = user.Username;
                }

                IWebhook webhook = textChannel.CreateWebhookAsync(nome).GetAwaiter().GetResult() as IWebhook;

                DiscordWebhookClient webhookClient = new DiscordWebhookClient(webhook);
                webhookClient.SendMessageAsync(msg);
                webhook.DeleteAsync().GetAwaiter().GetResult();
            }
        }
    }
}
