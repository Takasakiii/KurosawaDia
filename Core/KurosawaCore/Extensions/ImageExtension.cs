using KurosawaCore.Extensions.HttpExtension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class ImageExtension
    {
        private const string urlBaseNekosLife = "https://nekos.life/api/v2/img/meow";
        private const string urlBaseRandomDog = "https://random.dog/woof.json";
        private const string urlBaseLolisLife = "https://api.lolis.life/random";

        internal async Task<string> GetCat()
        {
            string url;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(urlBaseNekosLife);
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

            return url;
        }

        internal async Task<string> GetDog()
        {
            string url;

            do
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync(urlBaseRandomDog);
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        string json = await httpResponseMessage.Content.ReadAsStringAsync();
                        url = JsonConvert.DeserializeObject<RandomDog>(json).Url;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            } while (!await new HttpsExtension().IsImage(url));

            return url;
        }

        internal async Task<string> GetLoli()
        {
            string url;

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage httpResponseMessage = await client.GetAsync(urlBaseLolisLife);
                if (httpResponseMessage.IsSuccessStatusCode)
                {
                    string json = await httpResponseMessage.Content.ReadAsStringAsync();
                    url = JsonConvert.DeserializeObject<LolisLife>(json).Url;
                }
                else
                {
                    throw new Exception();
                }
            }

            return url;
        }

        internal async Task<string> GetLoliBomb()
        {
            string urls = "";

            for (int i = 0; i < 5; i++)
            {
                urls += await GetLoli() + "\n";
            }

            return urls;
        }
    }
}
