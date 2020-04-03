using DSharpPlus.Entities;
using KurosawaCore.Singletons;
using System.Threading.Tasks;
using Weeb.net;
using Weeb.net.Data;

namespace KurosawaCore.Extensions
{
    internal class WeebExtension
    {
        internal DiscordUser UsuarioDestino { get; set; } = null;
        internal string SelfMsg { get; set; } = "";
        internal bool Auto { get; set; } = true;
        internal DiscordUser Author { get; set; } = null;

        internal async Task<DiscordEmbed> GetWeeb(string tipo, string msg)
        {
            WeebClient weeb = new WeebClient();
            await weeb.Authenticate(DependencesSingleton.GetApiWeeb().Key, TokenType.Wolke);
            RandomData imgReceived = await weeb.GetRandomAsync(tipo, new string[] { }, FileType.Any, false, NsfwSearch.False);
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Color = DiscordColor.HotPink,
                ImageUrl = imgReceived.Url
            };

            if (Auto)
            {
                eb.WithTitle($"{Author.Username} {((UsuarioDestino == null) ? SelfMsg : msg + " " + UsuarioDestino.Username)}");
            }
            else
            {
                eb.WithTitle(msg);
            }

            return eb.Build();
        }
    }
}
