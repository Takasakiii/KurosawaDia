using Newtonsoft.Json;

namespace KurosawaCore.Extensions.NHentai.Modelos
{
    internal class Relacionados
    {
        [JsonProperty("result")]
        internal Doujin[] Doujins { get; set; }
    }
}
