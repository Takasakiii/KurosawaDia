using Newtonsoft.Json;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class HttpsExtension
    {
        internal static async Task<bool> IsImage(string url)
        {
            try
            {
                WebRequest req = WebRequest.Create(url);
                req.Method = "HEAD";
                using (WebResponse resp = await req.GetResponseAsync())
                {
                    return resp.ContentType.ToLower(CultureInfo.InvariantCulture).StartsWith("image/");
                }
            }
            catch
            {
                return false;
            }
        }

        internal static async Task<ObjResultante> PegarJsonGET<ObjResultante>(string url, params string[] args)
        {
            string json = await new WebClient().DownloadStringTaskAsync(string.Format(url, args));
            return JsonConvert.DeserializeObject<ObjResultante>(json);
        }
    }
}
