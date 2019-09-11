using Discord;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using Color = Discord.Color;

namespace Bot.Extensions
{
    class EmbedControl
    {
        public Tuple<string, Embed> CriarEmbedJson(string json)
        {
            EmbedTempModel embed = (EmbedTempModel)JsonConvert.DeserializeObject(json, typeof(EmbedTempModel));
            EmbedBuilder construtor = new EmbedBuilder();

            if (!string.IsNullOrWhiteSpace(embed.title))
                construtor.WithTitle(embed.title);
            if (!string.IsNullOrWhiteSpace(embed.description))
                construtor.WithDescription(embed.description);
            if (embed.author != null && !string.IsNullOrWhiteSpace(embed.author.name))
            {
                construtor.WithAuthor(autor =>
                {
                    if (Uri.IsWellFormedUriString(embed.author.icon_url, UriKind.Absolute))
                        autor.WithIconUrl(embed.author.icon_url);
                    if (Uri.IsWellFormedUriString(embed.author.url, UriKind.Absolute))
                        autor.WithUrl(embed.author.url);
                    autor.WithName(embed.author.name);
                });
            }
            construtor.WithColor(new Color(embed.color));
            if (embed.footer != null)
                construtor.WithFooter(embedfooter =>
                {
                    embedfooter.WithText(embed.footer.text);
                    if (Uri.IsWellFormedUriString(embed.footer.icon_url, UriKind.Absolute))
                        embedfooter.WithIconUrl(embed.footer.icon_url);
                });
            if (!string.IsNullOrWhiteSpace(embed.thumbnail) && Uri.IsWellFormedUriString(embed.thumbnail, UriKind.Absolute))
                construtor.WithThumbnailUrl(embed.thumbnail);
            if (!string.IsNullOrWhiteSpace(embed.image) && Uri.IsWellFormedUriString(embed.image, UriKind.Absolute))
                construtor.WithImageUrl(embed.image);
            if (embed.fields != null)
            {
                foreach (EmbedFildsModel fild in embed.fields)
                {
                    if (!string.IsNullOrWhiteSpace(fild.name) && !string.IsNullOrWhiteSpace(fild.value))
                        construtor.AddField(fildb => fildb.WithName(fild.name).WithValue(fild.value).WithIsInline(fild.inline));
                }
            }

            return Tuple.Create(embed.plainText, construtor.Build());
        }

        public void SendMessage(IMessageChannel canal, string valor)
        {
            try
            {
                var embed = CriarEmbedJson(valor);
                canal.SendMessageAsync(text: embed.Item1, embed: embed.Item2);
            }
            catch
            {
                canal.SendMessageAsync(valor);
            }
        }
    }

    public class EmbedTempModel
    {
        public string plainText;
        public string title;
        public string description;
        public EmbedAutorModel author;
        public uint color;
        public EmbedFooterModel footer;
        public string thumbnail;
        public string image;
        public EmbedFildsModel[] fields;
    }

    public class EmbedFooterModel
    {
        public string text;
        public string icon_url;
    }

    public class EmbedAutorModel
    {
        public string name;
        public string url;
        public string icon_url;
    }

    public class EmbedFildsModel
    {
        public string name;
        public string value;
        public bool inline;
    }
}
