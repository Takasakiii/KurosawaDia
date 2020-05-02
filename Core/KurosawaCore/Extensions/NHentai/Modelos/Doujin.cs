using KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes;
using Newtonsoft.Json;

namespace KurosawaCore.Extensions.NHentai.Modelos
{
    internal class Doujin
    {
        [JsonProperty("id")]
        internal ulong Id { get; set; }
        [JsonProperty("media_id")]
        internal string MediaId { get; set; }
        [JsonProperty("title")]
        internal Title Titulo { get; set; }
        [JsonProperty("tags")]
        internal Tags[] Tags { get; set; }
        [JsonProperty("num_pages")]
        internal ulong TotalPaginas { get; set; }
        [JsonProperty("num_favorites")]
        internal ulong Favoritos { get; set; }
    }
}
