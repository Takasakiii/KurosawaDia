using DSharpPlus.Entities;
using System;
using System.Globalization;
using System.Net;
using Neo = NeoSmart.Unicode.Emoji;
using System.Threading.Tasks;
using System.Collections.Generic;
using NeoSmart.Unicode;

namespace KurosawaCore.Extensions
{
    internal class DiscordEmojiExtension
    {
        private const string BaseUrl = "https://cdn.discordapp.com/emojis/{0}";
        internal DiscordEmoji Emoji { private set; get; }
        internal DiscordEmojiExtension(DiscordEmoji emoji)
        {
            Emoji = emoji;
        }


        internal async Task<string> GetUrl()
        {
            if(!Neo.IsEmoji(Emoji, 1))
            {
                if (Emoji.Id == 0)
                    throw new InvalidOperationException("Cannot get URL of unicode emojis.");

                if (await GetAnimated())
                    return string.Format(BaseUrl, $"{Emoji.Id.ToString(CultureInfo.InvariantCulture)}.gif");
                return string.Format(BaseUrl, $"{Emoji.Id.ToString(CultureInfo.InvariantCulture)}.png");
            }
            else
            {
                List<string> hexSeq = new List<string>();
                foreach (uint seq in Emoji.ToString().AsUnicodeSequence().AsUtf32)
                {
                    hexSeq.Add(seq.ToString("x"));
                }
                return $"https://twemoji.maxcdn.com/2/72x72/{string.Join('-', hexSeq)}.png";
            }
        }

        internal async Task<bool> GetAnimated()
        {
            return await IsImageUrl(string.Format(BaseUrl, $"{Emoji.Id.ToString(CultureInfo.InvariantCulture)}.gif"));
        }

        private async Task<bool> IsImageUrl(string URL)
        {
            try
            {
                WebRequest req = WebRequest.Create(URL);
                req.Method = "HEAD";
                using (WebResponse resp = await req.GetResponseAsync())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
                }
            }
            catch
            {
                return false;
            }
        }


    }
}
