using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Nsfw", "🔞")]
    [Description("Este módulo possui coisas para você dar orgulho para sua família.")]
    public class Nsfw
    {
        [Command("hentai")]
        [Description("Consiga uma imagem que façam com que sua família se orgulhe aqui.")]
        [RequireNsfw]
        public async Task Hentai(CommandContext ctx)
        {
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                ImageUrl = await new NfswExtension().GetHentai(ctx.Guild.Id),
                Color = DiscordColor.Lilac
            });
        }

        [Command("hentaibomb")]
        [Description("Consiga imagens que façam com que sua família se orgulhe aqui.")]
        [RequireNsfw]
        public async Task HentaiBomb(CommandContext ctx)
        {
            await ctx.RespondAsync(await new NfswExtension().GetHentais(ctx.Guild.Id));
        }
    }
}
