using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class HttpsExtension
    {
        internal async Task<bool> IsImage(string url)
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
    }
}
