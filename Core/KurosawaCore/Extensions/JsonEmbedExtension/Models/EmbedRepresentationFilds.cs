using Newtonsoft.Json;

namespace KurosawaCore.Extensions.JsonEmbedExtension.Models
{
    internal class EmbedRepresentationFilds
    {
        [JsonProperty("name")]
        internal string Name { get; set; }
        [JsonProperty("value")]
        internal string Value { get; set; }
        [JsonProperty("inline")]
        internal bool InLine { get; set; }
    }
}
