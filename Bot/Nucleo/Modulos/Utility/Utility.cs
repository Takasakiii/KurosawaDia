using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Weeb.net;

namespace Bot.Nucleo.Modulos
{
    public class Utility
    {
        CommandContext context;

        public Utility(CommandContext context)
        {
            this.context = context;
        }

        //coerencia
        public async Task Ping()
        {
            Stopwatch sw = Stopwatch.StartNew(); 
            IUserMessage msg = await context.Channel.SendMessageAsync("🏓").ConfigureAwait(false);
            sw.Stop();
            msg.DeleteAfter(0);
            
            await context.SendConfirmAsync($"🏓 {(int)sw.Elapsed.TotalMilliseconds}ms").ConfigureAwait(false);
            //ve sa porra 
        }


        public async Task Avatar(DiscordSocketClient client, string[] comando)
        {
            SocketUser user = context.GetUser(client, comando);

            string avatarUrl = user.GetAvatarUrl(0, 2048) ?? user.GetDefaultAvatarUrl(); //mais um dado do Objeto usuario (criar) souto (¯\_(ツ)_/¯)(¯\_(ツ)_/¯)

            EmbedBuilder builder = new EmbedBuilder()
                .WithAuthor($"{user}")
                .WithDescription($"[Link Direto]({avatarUrl})")
                .WithImageUrl(avatarUrl)
                .WithOkColor();
            Embed embed = builder.Build();

            await context.Channel.SendMessageAsync("", embed: embed).ConfigureAwait(false);

            //generalizar
        }

        public async Task WebCam()
        {
            SocketGuildUser usr = context.User as SocketGuildUser;

            if(usr.VoiceChannel != null)
            {
                await context.SendConfirmAsync($"[clique aqui](https://discordapp.com/channels/{context.Guild.Id}/{usr.VoiceChannel.Id}) para poder compartilhar sua tela ou ligar sua webcam");
            } else
            {
                await context.SendErrorAsync("você precisa estar em um canal de voz para usar esse comando");
            }
        }
    }
}
