using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using KurosawaCore.Extensions.HttpExtension;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal class NfswExtension
    {
        private const string urlBaseNekoBot = "https://nekobot.xyz/api/image?type=";
        private const string urlBaseNekosLife = "https://nekos.life/api/v2/img/";
        private const string urlBaseLolisLife = "https://api.lolis.life/random?category=";

        private enum NekoBotType
        {
            hentai,
            hanal
        }

        private enum NekosLifeType
        {
            lewdk,
            nsfw_neko_gif
        }

        private enum LolisLifeType
        {
            slave,
            lewd,
            monster
        }

        internal async Task<string> GetHentai(ulong guilId)
        {
            string url;
            int escolha;
            if ((await new ServidoresDAO().Get(new Servidores { ID = guilId})).Espercial == TiposServidores.LolisEdition)
                escolha = new Random().Next(3);
            else
                escolha = new Random().Next(2);
            

            if (escolha == 1)
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
            else if (escolha == 2)
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
            else
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage httpResponseMessage = await client.GetAsync($"{urlBaseLolisLife}{(LolisLifeType)new Random().Next(4)}");
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
            }

            return url;
        }

        internal async Task<string> GetHentais(ulong guilId)
        {
            string urls = "";

            for (int i = 0; i < 5; i++)
            {
                urls += await GetHentai(guilId) + "\n";
            }

            return urls;
        }
    }
}
