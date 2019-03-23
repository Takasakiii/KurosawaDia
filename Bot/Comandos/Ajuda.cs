using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace Bot.Comandos
{
    public class Ajuda : Image
    {
        public void ajuda(CommandContext context, object[] args)
        {
            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                .WithColor(Color.DarkPurple)
                .WithDescription($"Oii {context.User} você pode usar `{(string)args[0]}comandos` para ver os comandos que eu tenho <:hehe:555914678866280448>")

                .Build());
        }

        public void comandos(CommandContext context, object[] args)
        {
            Embed embed = new EmbedBuilder()
                .WithTitle("Esses são os meus comandos")
                .AddField("Comandos de Utilidades:", $"`{(string)args[0]}webcam`, `{(string)args[0]}avatar`, `{(string)args[0]}emote`")
                .AddField("Comandos de Ajuda:", $"`{(string)args[0]}ajuda`, `{(string)args[0]}comandos`")
                .AddField("Comandos de Imagens", $"`{(string)args[0]}neko`, `{(string)args[0]}cat`, `{(string)args[0]}neko`")
                .AddField("Comandos NSFW", $"`{(string)args[0]}hentai`")
                .AddField("Comandos Weeb", $"`{(string)args[0]}hug`, `{(string)args[0]}slap`, `{(string)args[0]}kiss`, `{(string)args[0]}punch`, `{(string)args[0]}lick`")
                .WithThumbnailUrl(context.Client.CurrentUser.GetAvatarUrl(0, 2048))
                .WithColor(Color.DarkPurple)
                .Build();

            if(!context.IsPrivate)
            {
                SocketGuildUser user = context.User as SocketGuildUser;
                IDMChannel prive = user.GetOrCreateDMChannelAsync().GetAwaiter().GetResult();
                context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithColor(Color.DarkPurple)
                    .WithDescription($"**{context.User}** eu enviarei a lista dos meus comandos no seu privado 😜")
                    .Build());
                prive.SendMessageAsync(embed: embed);
            } else
            {
                context.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
