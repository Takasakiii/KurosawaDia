using Bot.Extensions;
using Discord;
using Discord.Commands;

namespace Bot.Comandos
{
    public class Image : Nsfw
    {
        public void neko(CommandContext context, object[] args)
        {
            string url = new HttpExtensions().GetSite("https://nekos.life/api/v2/img/neko", "url").GetAwaiter().GetResult();

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Nekos")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void cat(CommandContext context, object[] args)
        {
            string url = new HttpExtensions().GetSite("https://nekos.life/api/v2/img/meow", "url").GetAwaiter().GetResult();

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Meow")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void dog(CommandContext context, object[] args)
        {
            string url = new HttpExtensions().GetSite("https://random.dog/woof.json", "url").GetAwaiter().GetResult();

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Woof")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
