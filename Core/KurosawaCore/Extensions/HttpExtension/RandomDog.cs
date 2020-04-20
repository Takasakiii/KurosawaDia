using Newtonsoft.Json;

namespace KurosawaCore.Extensions.HttpExtension
{
    internal class RandomDog
    {
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
