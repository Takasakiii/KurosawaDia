using Newtonsoft.Json;

namespace KurosawaCore.Extensions.HttpExtension
{
    internal class NekoBot
    {
        [JsonProperty("message")]
        public string Url { get; set; }
    }
}
