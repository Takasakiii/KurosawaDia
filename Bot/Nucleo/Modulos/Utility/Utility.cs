using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Bot.Nucleo.Modulos
{
    public class Utility
    {
        CommandContext context;

        public Utility(CommandContext context)
        {
            this.context = context;
        }

        public async Task Avatar(string[] comando)
        {
            IUser user;
            try
            {
                if (context.Message.MentionedUserIds.Count != 0)
                {
                    user = await context.Client.GetUserAsync(context.Message.MentionedUserIds.ElementAt(0));
                } else
                {
                    user = await context.Client.GetUserAsync(Convert.ToUInt64(comando[1]));
                }
            } catch
            {
                user = context.User;
            }
            string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl();

            await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithAuthor($"{user}")
                .WithDescription($"[Link Direto]({avatarUrl})")
                .WithImageUrl(avatarUrl)
            .Build());
        }

        public async Task WebCam()
        {
            SocketGuildUser usr = context.User as SocketGuildUser;

            if(!context.IsPrivate && usr.VoiceChannel != null)
            {
                await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.DarkPurple)
                        .WithDescription($"[clique aqui](https://discordapp.com/channels/{context.Guild.Id}/{usr.VoiceChannel.Id}) para poder compartilhar sua tela ou ligar sua webcam")
                .Build());
            } else
            {
                await context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                        .WithColor(Color.Red)
                        .WithDescription("você precisa estar em um canal de voz e em um servidor para usar esse comando")
                .Build());
            }
        }
    }
}
