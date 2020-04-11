using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                if (!await new HttpsExtension().IsImage(url))
                    url = guild.IconUrl;
            }
            else
                url = guild.IconUrl;

            return $"{url}?size=2048";
        }
    }
}
