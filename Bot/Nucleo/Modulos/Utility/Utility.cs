using Bot.Nucleo.Extensions;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
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

            string avatarUrl = "aa";

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
