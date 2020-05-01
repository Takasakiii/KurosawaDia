using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System.Collections.Generic;
using System.Linq;

namespace KurosawaCore.Extensions
{
    internal class PagesExtensions
    {
        internal List<Page> Paginador { get; private set; }
        internal PagesExtensions(IEnumerable<Page> paginador)
        {
            Paginador = paginador.ToList();
        }

        internal PagesExtensions()
        {
            Paginador = new List<Page>();
        }

        internal void AdicionarPaginaString(string content)
        {
            Paginador.Add(new Page
            {
                Content = content
            });
        }

        internal void AdicionarEmbed(DiscordEmbed embed)
        {
            Paginador.Add(new Page
            {
                Embed = embed
            });
        }

        internal void AdicionarPage(Page pagina)
        {
            Paginador.Add(pagina);
        }
    }
}
