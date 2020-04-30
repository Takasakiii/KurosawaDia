using NeoSmart.Unicode;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Neo = NeoSmart.Unicode.Emoji;

namespace KurosawaCore.Extensions
{
    internal class DiscordEmojiExtension
    {
        private const string BaseUrl = "https://cdn.discordapp.com/emojis/{0}";
        internal ulong ID { private set; get; }
        internal string Nome { private set; get; }

        internal bool IsGuildEmoji { private set; get; } = true;

        internal bool Animated { private set; get; } = false;

        internal DiscordEmojiExtension(string emoji)
        {
            Nome = emoji;
            Regex r = new Regex(@"<a?:(?<nome>\w{1,32}):(?<id>\d{18})>");
            Match match = r.Match(emoji);
            if (match.Success)
            {
                ID = ulong.Parse(match.Groups["id"].Value);
                Nome = match.Groups["nome"].Value;
                if (emoji[1] == 'a')
                {
                    Animated = true;
                }
            }
            else
            {
                IsGuildEmoji = false;
            }
        }


        internal string GetUrl()
        {
            if (!IsGuildEmoji)
                if (Neo.IsEmoji(Nome))
                {
                    List<string> hexSeq = new List<string>();
                    foreach (uint seq in Nome.AsUnicodeSequence().AsUtf32)
                    {
                        hexSeq.Add(seq.ToString("x"));
                    }
                    return $"https://twemoji.maxcdn.com/2/72x72/{string.Join('-', hexSeq)}.png";
                }

            if (ID == 0)
                throw new Exception("Emoji invalido");

            if (Animated)
                return string.Format(BaseUrl, $"{ID.ToString(CultureInfo.InvariantCulture)}.gif");
            else
                return string.Format(BaseUrl, $"{ID.ToString(CultureInfo.InvariantCulture)}.png");
        }

    }
}
