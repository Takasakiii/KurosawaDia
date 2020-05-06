using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using KurosawaCore.Extensions.NHentai.Modelos;
using KurosawaCore.Extensions.NHentai.Modelos.DoujinAtributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        private const string Doujin = "/api/gallery/{0}";
        private const string Pagina = "/galleries/{0}/{1}.{2}";


        private CommandContext Context;

        internal NHentaiExtension(CommandContext context)
        {
            Context = context;
        }

        internal async Task LerDoujin(uint codigo)
        {
            await Task.Yield();
            bool permissao = false;

            Doujin dou = await HttpsExtension.PegarJsonGET<Doujin>(BaseURL + Doujin, codigo.ToString());

            permissao = (byte)(await new ServidoresDAO().Get(new Servidores
            {
                ID = Context.Guild.Id
            })).Especial >= (byte)TiposServidores.LolisEdition;

            if (!permissao && dou.Tags.Where(x => x.Id == 19440 || x.Id == 32241).Count() > 0)
                throw new Exception("Sem permissao loli");

            IEnumerable<Doujin> relacionados;
            if (!permissao)
                relacionados = (await HttpsExtension.PegarJsonGET<Relacionados>(BaseURL + Relacionados, codigo.ToString())).Doujins.Where(x =>x.Tags.Where(y => y.Id != 19440 && y.Id != 32241).Count() == 0);
            else
                relacionados = (await HttpsExtension.PegarJsonGET<Relacionados>(BaseURL + Relacionados, codigo.ToString())).Doujins;

            StringBuilder sb = new StringBuilder();
            foreach (Doujin temp in relacionados)
            {
                sb.AppendLine($"- [{temp.Titulo.Abreviacao} ({temp.Id})]({BaseURL}/g/{temp.Id})");
            }

            PagesExtensions pages = new PagesExtensions();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = WebUtility.HtmlDecode(dou.Titulo.NomeIngles),
                Url = $"{BaseURL}/g/{dou.Id}",
                Color = DiscordColor.Lilac,
                ImageUrl = GetImageUrl(dou, 1)
            };


            IEnumerable<Tags> tags = dou.Tags.Where(x => x.Tipo == "artist" || x.Tipo == "group");
            string artistName = tags.Where(x => x.Tipo == "artist").FirstOrDefault()?.Nome;
            if (!string.IsNullOrWhiteSpace(artistName))
            {
                eb.AddField("Autor:", artistName, true);
            }

            Tags grupo;
            if ((grupo = tags.Where(x => x.Tipo == "group").FirstOrDefault()) != null)
            {
                eb.AddField("Grupo:", grupo.Nome, true);
            }
            eb.AddField("Votos:", dou.Favoritos.ToString(), true);
            string srela = sb.ToString();
            if (!string.IsNullOrEmpty(srela))
            {
                eb.AddField("Relacionados:", WebUtility.HtmlDecode(srela), true);
            }
            pages.AdicionarEmbed(eb);

            for (ulong i = 2; i < dou.TotalPaginas; i++)
            {
                string imgurl = GetImageUrl(dou, Convert.ToInt32(i));
                pages.AdicionarEmbed(new DiscordEmbedBuilder
                {
                    Title = $"{i - 1}/{dou.TotalPaginas - 2}",
                    Url = imgurl,
                    ImageUrl = imgurl,
                    Color = DiscordColor.Lilac
                });
            }

            await Context.Client.GetInteractivityModule().SendPaginatedMessage(Context.Channel, Context.User, pages.Paginador, TimeSpan.FromMinutes(10));
        }

        private string GetImageUrl (Doujin doujin, int posicao)
        {
            return BaseCdnUrl + string.Format(Pagina, doujin.MediaId, posicao, (doujin.Imagens.Paginas[posicao - 1].Tipo == "j") ? "jpg" : "png");
        }

        internal async Task Pesquisar (string query)
        {
            await Task.Yield();


        }

    }
}
