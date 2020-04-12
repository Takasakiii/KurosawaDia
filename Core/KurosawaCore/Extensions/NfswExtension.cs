using KurosawaCore.Extensions.HttpExtension;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class NfswExtension
    {
        const string urlBaseNekoBot = "https://nekobot.xyz/api/image?type=";
        const string urlBaseNekosLife = "https://nekos.life/api/v2/img/";

        internal enum NekoBotType
        {
            hentai,
            hanal
        }

        internal enum NekosLifeType
        {
            lewdk,
            nsfw_neko_gif
        }

        internal async Task<string> GetHentai()
        {
            string url;

            if (new Random().Next(2) == 1)
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync($"{urlBaseNekoBot}{(NekoBotType)new Random().Next(2)}");
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string json = await httpResponseMessage.Content.ReadAsStringAsync();
                        url = JsonConvert.DeserializeObject<NekoBot>(json).Url;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync($"{urlBaseNekosLife}{(NekosLifeType)new Random().Next(2)}");
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string json = await httpResponseMessage.Content.ReadAsStringAsync();
                        url = JsonConvert.DeserializeObject<NekosLife>(json).Url;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }

            return url;
        }

        internal async Task<string> GetHentais()
        {
            string urls = "";

            for (int i = 0; i < 5; i++)
            {
                urls += await GetHentai() + "\n";
            }

            return urls;
        }
    }
}
