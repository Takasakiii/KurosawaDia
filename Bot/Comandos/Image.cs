using Bot.Extensions;
using Discord;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Image : Nsfw
    {
        HttpExtensions http = new HttpExtensions();
        public void neko(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/neko", "url");

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Nekos")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void cat(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/meow", "url");

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Meow")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void dog(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://random.dog/woof.json", "url");

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Woof")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void img(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/avatar", "url");

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Uma simples imagem para usar onde quiser (ou não kerek)")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
