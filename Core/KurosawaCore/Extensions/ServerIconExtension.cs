using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class ServerIconExtension
    {
        internal async Task<string> Get(DiscordGuild guild)
        {
            string url;
            if (guild.Features.Contains("ANIMATED_ICON"))
            {
                url = guild.IconUrl.Replace(".jpg", ".gif");
                if (!await HttpsExtension.IsImage(url))
                    url = guild.IconUrl;
            }
            else
                url = guild.IconUrl;

            return $"{url}?size=2048";
        }
    }
}
