using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    public class HttpExtension
    {
        public async Task<string> GetSite(string url, string parametro)
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
