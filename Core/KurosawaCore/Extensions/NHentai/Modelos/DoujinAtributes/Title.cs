using Newtonsoft.Json;

namespace KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes
{
    internal class Title
    {
        [JsonProperty("english")]
        internal string NomeIngles { get; set; }
        [JsonProperty("japanese")]
        internal string NomeJapones { get; set; }
        [JsonProperty("pretty")]
        internal string Abreviacao { get; set; }
    }
}
