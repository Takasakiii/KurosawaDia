using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Nfsw", "🔞")]
    [Description("Este módulo possui coisas para você dar orgulho para sua família.")]
    public class Nfsw
    {
        [Command("hentai")]
        [Description("Consiga uma bela imagem aqui")]
        [RequireNsfw]
        public async Task Hentai(CommandContext ctx)
        {
            await ctx.RespondAsync(await new NfswExtension().GetHentai(ctx.Guild.Id));
        }

        [Command("hentaibomb")]
        [Description("Consiga belas imagens aqui")]
        [RequireNsfw]
        public async Task HentaiBomb(CommandContext ctx)
        {
            await ctx.RespondAsync(await new NfswExtension().GetHentais(ctx.Guild.Id));
        }
    }
}
