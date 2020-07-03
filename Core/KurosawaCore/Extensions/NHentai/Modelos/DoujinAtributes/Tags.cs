using Newtonsoft.Json;

namespace KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes
{
    internal class Tags
    {
        [JsonProperty("id")]
        internal ulong Id { get; set; }
        [JsonProperty("type")]
        internal string Tipo { get; set; }
        [JsonProperty("name")]
        internal string Nome { get; set; }
        [JsonProperty("url")]
        internal string Url { get; set; }
        [JsonProperty("count")]
        internal ulong Quantidade { get; set; }
    }
}
