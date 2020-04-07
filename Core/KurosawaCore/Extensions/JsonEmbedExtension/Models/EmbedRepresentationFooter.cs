using Newtonsoft.Json;

namespace KurosawaCore.Extensions.JsonEmbedExtension.Models
{
    internal class EmbedRepresentationFooter
    {
        [JsonProperty("text")]
        internal string Text { get; set; }
        [JsonProperty("icon_url")]
        internal string IconUrl { get; set; }
    }
}
