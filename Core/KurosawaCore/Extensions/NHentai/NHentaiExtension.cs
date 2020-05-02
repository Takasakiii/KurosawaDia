using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using KurosawaCore.Extensions.NHentai.Modelos;
using KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions.NHentai
{
    internal sealed class NHentaiExtension
    {
        private const string BaseURL = "https://nhentai.net";
        private const string BaseCdnUrl = "https://i.nhentai.net";
        private const string Pesquisar = "/api/galleries/search?query={0}";
        private const string Relacionados = "/api/gallery/{0}/related";
        private const string Tag = "/api/galleries/tagged?tag_id={0}";
        private const string Doujin = "/api/gallery/{0}";
        private const string Pagina = "/galleries/{0}/{1}.jpg";
        private const string Image = "/galleries/{0}/{1}t.jpg";
        private const string Cover = "/galleries/{0}/1.jpg";


        private CommandContext Context;

        internal NHentaiExtension(CommandContext context)
        {
            Context = context;
        }

        internal async Task LerDoujin(uint codigo)
        {
            await Task.Yield();

            Doujin dou = await HttpsExtension.PegarJsonGET<Doujin>(BaseURL + Doujin, codigo.ToString());
            Relacionados relacionados = await HttpsExtension.PegarJsonGET<Relacionados>(BaseURL + Relacionados, codigo.ToString());

            StringBuilder sb = new StringBuilder();
            foreach(Doujin temp in relacionados.Doujins)
            {
                sb.AppendLine($"- [{temp.Titulo.Abreviacao} ({temp.Id})]({BaseURL}/g/{temp.Id})");
            }

            PagesExtensions pages = new PagesExtensions();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = dou.Titulo.NomeIngles,
                Url = $"{BaseURL}/g/{dou.Id}",
                Color = DiscordColor.Lilac,
                ImageUrl = string.Format(BaseCdnUrl + Cover, dou.MediaId)
            };


            IEnumerable<Tags> tags = dou.Tags.Where(x => x.Tipo == "artist" || x.Tipo == "group");
            eb.AddField("Autor:", tags.Where(x => x.Tipo == "artist").FirstOrDefault().Nome, true);
            eb.AddField("Grupo:", tags.Where(x => x.Tipo == "group").FirstOrDefault().Nome, true);
            eb.AddField("Votos:", dou.Favoritos.ToString(), true);
            eb.AddField("Relacionados:", sb.ToString(), true);
            pages.AdicionarEmbed(eb);

            for (ulong i = 2; i < dou.TotalPaginas; i++)
            {
                string urlImage = BaseCdnUrl + string.Format(Pagina, dou.MediaId, i);
                pages.AdicionarEmbed(new DiscordEmbedBuilder
                {
                    Title = $"{i - 1}/{dou.TotalPaginas - 1}",
                    Url = urlImage,
                    ImageUrl = urlImage,
                    Color = DiscordColor.Lilac
                });
            }

            await Context.Client.GetInteractivityModule().SendPaginatedMessage(Context.Channel, Context.User, pages.Paginador, TimeSpan.FromMinutes(10));
        }
    }
}
