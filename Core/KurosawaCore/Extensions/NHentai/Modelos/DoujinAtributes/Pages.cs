using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes
{
    internal class Pages
    {
        [JsonProperty("t")]
        internal string Tipo { get; set; }
    }
}
