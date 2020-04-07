using DSharpPlus.Entities;
using KurosawaCore.Extensions.JsonEmbedExtension.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions.JsonEmbedExtension
{
    internal class JsonEmbedExtension
    {
        internal virtual DiscordEmbed GetJsonEmbed(ref string message)
        {
            EmbedRepresentation embedToBuild = JsonConvert.DeserializeObject<EmbedRepresentation>(message);
            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = embedToBuild.Title,
                Description = embedToBuild.Description,
                Color = new DiscordColor(embedToBuild.Color),
                ThumbnailUrl = embedToBuild.Thumbnail,
                ImageUrl = embedToBuild.Image
            };
            if (embedToBuild.Author != null)
                eb.WithAuthor(embedToBuild.Author.Name, embedToBuild.Author.Url, embedToBuild.Author.IconUrl);
            if (embedToBuild.Footer != null)
                eb.WithFooter(embedToBuild.Footer.Text, embedToBuild.Footer.IconUrl);
            foreach (EmbedRepresentationFilds field in embedToBuild.Fields)
                eb.AddField(field.Name, field.Value, field.InLine);
            message = embedToBuild.PlainText;
            return eb.Build();
        }

        internal virtual async Task SendMessage(DiscordChannel canal, string msg)
        {
            try
            {
                DiscordEmbed embed = GetJsonEmbed(ref msg);
                await canal.SendMessageAsync(msg, embed: embed);
            }
            catch
            {
                await canal.SendMessageAsync(msg);
            }
        }
    }
}
