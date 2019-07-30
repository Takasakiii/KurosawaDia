using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Net.Http;

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

        public string GetSiteHttp(string url, string parametro)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string site = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                JToken siteJson = JObject.Parse(site);

                return siteJson.SelectToken(parametro).ToString();
            }
        }

        public bool IsImageUrl(string URL)
        {
            try
            {
                WebRequest req = WebRequest.Create(URL);
                req.Method = "HEAD";
                using (WebResponse resp = req.GetResponse())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
