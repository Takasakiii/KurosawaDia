using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KurosawaCore.Extensions
{
    internal class ServerIconExtension
    {
        internal string Get(DiscordGuild guild)
        {
            string url;
            if (guild.Features.Contains("ANIMATED_ICON"))
                url = $"{guild.IconUrl.Replace(".jpg", ".gif")}?size=2048";
            else
                url = $"{guild.IconUrl}?size=2048";

            return url;
        }
    }
}
