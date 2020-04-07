using Newtonsoft.Json;

namespace KurosawaCore.Extensions.JsonEmbedExtension.Models
{
    internal class EmbedRepresentation
    {
        [JsonProperty("plainText")]
        internal string PlainText { get; set; }
        [JsonProperty("title")]
        internal string Title { get; set; }
        [JsonProperty("description")]
        internal string Description { get; set; }
        [JsonProperty("author")]
        internal EmbedRepresentarionAuthor Author { get; set; }
        [JsonProperty("color")]
        internal int Color { get; set; }
        [JsonProperty("footer")]
        internal EmbedRepresentationFooter Footer { get; set; }
        [JsonProperty("thumbnail")]
        internal string Thumbnail { get; set; }
        [JsonProperty("image")]
        internal string Image { get; set; }
        [JsonProperty("fields")]
        internal EmbedRepresentationFilds[] Fields { get; set; }
    }
}
