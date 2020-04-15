using Newtonsoft.Json;

namespace KurosawaCore.Extensions.JsonEmbedExtension.Models
{
    internal class EmbedRepresentarionAuthor
    {
        [JsonProperty("name")]
        internal string Name { get; set; }
        [JsonProperty("url")]
        internal string Url { get; set; }
        [JsonProperty("icon_url")]
        internal string IconUrl { get; set; }
    }
}
