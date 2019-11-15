using Newtonsoft.Json.Linq;
using System;
using System.Globalization;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    //Classe responsavel por manipular dados de apis http
    public class HttpExtensions : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest web = base.GetWebRequest(address);
            web.Timeout = int.MaxValue;
            return web;
        }


        //Metodo responsavel por baixar um dado de um json de resposta de uma api externa atravez do WebClient
        public async Task<string> GetSite(string url, string parametro)
        {
            return await Task.Run(() =>
            {
                using (WebClient wc = new WebClient())
                {
                    
                    string site = wc.DownloadString(url);
                    JToken siteJson = JObject.Parse(site);

                    return siteJson.SelectToken(parametro).ToString();
                }
            });
        }

        //Metodo responsavel por baixar um dado de um json de resposta de uma api externa atraves do HttpClient
        public async Task<string> GetSiteHttp(string url, string parametro)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                string site = await httpClient.GetStringAsync(url);
                JToken siteJson = JObject.Parse(site);

                return siteJson.SelectToken(parametro).ToString();
            }
        }

        //Metodo responsavel por verificar se uma url é responsavel por uma imagem
        public async Task<bool> IsImageUrl(string URL)
        {
            try
            {
                WebRequest req = WebRequest.Create(URL);
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

        //Metodo responsavel por verificar o tamanho do arquivo de uma url (via download simples)
        public async Task<Tuple<bool, long>> PegarTamanhoArquivo (string url)
        {
            bool retorno = false;
            long tamanho;
            WebRequest requisicao = WebRequest.Create(url);
            using(WebResponse resp = await requisicao.GetResponseAsync())
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
            return Tuple.Create(retorno, tamanho);
        }
    }
}
