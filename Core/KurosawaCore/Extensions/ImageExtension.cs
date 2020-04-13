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
        private const string urlBaseNekosLife = "https://nekos.life/api/v2/img/";
        private const string urlBaseRandomDog = "https://random.dog/woof.json";
        private const string urlBaseLolisLife = "https://api.lolis.life/random";

        private enum NekosLifeType
        {
            neko,
            meow
        }

        internal async Task<string> GetCat()
        {
            string url;

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
            } while (url.Contains(".mp4"));

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
    }
}
