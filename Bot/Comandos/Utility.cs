using Bot.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;

namespace Bot.Nucleo.Modulos
{
    public class Utility : Owner.Owner
    {
        public void avatar(CommandContext context, object[] args)
        {
            IUser user;
            try
            {
                if (context.Message.MentionedUserIds.Count != 0)
                {
                    user = context.Client.GetUserAsync(context.Message.MentionedUserIds.ElementAt(0)).GetAwaiter().GetResult();
                }
                else
                {
                    string[] comando = (string[])args[1];
                    user = context.Client.GetUserAsync(Convert.ToUInt64(comando[1])).GetAwaiter().GetResult();
                }
            }
            catch
            {
                user = context.User;
            }

            if (user == null)
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
            try
            {
                string[] comando = (string[])args[1];
                Emote emote = Emote.Parse(comando[1]);

                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                         .WithTitle(emote.Name)
                         .WithUrl(emote.Url)
                         .WithImageUrl(emote.Url)
                         .WithColor(Color.DarkPurple)
                     .Build());
            }
            catch
            {
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithDescription($"**{context.User}** o emoji que você tentou usar é inválido")
                        .AddField("Uso do comando: ", $"`{(string)args[0]}emote emoji`")
                        .AddField("Exemplo: ", $"`{(string)args[0]}emote :kanna:`")
                        .WithColor(Color.Red)
                    .Build());
            }
        }
    }
}
