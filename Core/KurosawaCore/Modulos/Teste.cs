using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Teste Modulo")]
    [Description("Parte de teste e instavel, use por sua conta em risco")]
    [Hidden]
    public class Teste
    {
        [Command("page")]
        [Description("testa paginators")]
        public async Task PaginatorTest(CommandContext ctx)
        {
            InteractivityModule interativy = ctx.Client.GetInteractivityModule();

            PagesExtensions pgs = new PagesExtensions();
            pgs.AdicionarPaginaString("pg 1");
            pgs.AdicionarPaginaString("pg 2");
            pgs.AdicionarPaginaString("pg 3");

            // send the paginator
            await interativy.SendPaginatedMessage(ctx.Channel, ctx.User, pgs.Paginador, TimeSpan.FromMinutes(5), TimeoutBehaviour.Delete);
        }
    }
}
