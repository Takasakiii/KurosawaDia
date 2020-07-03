using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes
{
    internal class Images
    {
        [JsonProperty("pages")]
        internal Pages[] Paginas { get; set; }
    }
}
