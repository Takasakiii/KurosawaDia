using Newtonsoft.Json.Linq;
using System.Globalization;
using System.Net;
using System.Net.Http;

namespace Bot.Extensions
{
    //Classe responsavel por manipular dados de apis http
    public class HttpExtensions
    {
        //Metodo responsavel por baixar um dado de um json de resposta de uma api externa atravez do WebClient
        public string GetSite(string url, string parametro)
        {
            using (WebClient wc = new WebClient())
            {
                string site = wc.DownloadString(url);
                JToken siteJson = JObject.Parse(site);

                return siteJson.SelectToken(parametro).ToString();
            }
        }

        //Metodo responsavel por baixar um dado de um json de resposta de uma api externa atraves do HttpClient
        public string GetSiteHttp(string url, string parametro)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string site = httpClient.GetStringAsync(url).GetAwaiter().GetResult();
                JToken siteJson = JObject.Parse(site);

                return siteJson.SelectToken(parametro).ToString();
            }
        }

        //Metodo responsavel por verificar se uma url é responsavel por uma imagem
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

        //Metodo responsavel por verificar o tamanho do arquivo de uma url (via download simples)
        public bool PegarTamanhoArquivo (string url, out long tamanho)
        {
            bool retorno = false;
            WebRequest requisicao = WebRequest.Create(url);
            using(WebResponse resp = requisicao.GetResponse())
            {
                if(resp.ContentLength > 0)
                {
                    retorno = true;
                    tamanho = resp.ContentLength;
                }
                else
                {
                    tamanho = -1;
                }
            }
            return retorno;
        }
    }
}
