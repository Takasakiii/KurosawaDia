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
            string url = http.GetSite("https://nekos.life/api/v2/img/neko", "url"); //this is a constructor

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Um pouco de meninas gato (ou gatos com skin) sempre faz bem")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void cat(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/meow", "url"); //construtor com repetição

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Meow")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void dog(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://random.dog/woof.json", "url"); //¯\_(ツ)_/¯

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Woof")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }

        public void img(CommandContext context, object[] args)
        {
            string url = http.GetSite("https://nekos.life/api/v2/img/avatar", "url"); //¯\_(ツ)_/¯

            context.Channel.SendMessageAsync(embed: new EmbedBuilder()
                    .WithTitle("Uma simples imagem para usar onde quiser (ou não kerek)")
                    .WithUrl(url)
                    .WithImageUrl(url)
                    .WithColor(Color.DarkPurple)
                .Build());
        }
    }
}
