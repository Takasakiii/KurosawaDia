using Newtonsoft.Json.Linq;
using System.Net;

namespace Bot.Extensions
{
    public class HttpExtensions
    {
        public string GetSite(string url, string parametro)
        {
            using (WebClient wc = new WebClient())
            {
                string site = wc.DownloadString(url);
                JToken siteJson = JObject.Parse(site);

                return siteJson.SelectToken(parametro).ToString();
            }
        }
    }
}
