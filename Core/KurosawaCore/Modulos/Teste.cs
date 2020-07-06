using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity;
using KurosawaCore.Extensions.NHentai;
using KurosawaCore.Models.Atributes;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    public class Teste
    {
        [Command("page")]
        [Description("testa paginators\n\n(Observação: precisa ser mt gay pra usar isso.)")]
        public async Task PaginatorTest(CommandContext ctx, uint codigo)
        {

            //await new NHentaiExtension(ctx).LerDoujin(codigo);
            //PagesExtensions pgs = new PagesExtensions();
            //pgs.AdicionarPaginaString("pg 1");
            //pgs.AdicionarPaginaString("pg 2");
            //pgs.AdicionarPaginaString("pg 3");

            //// send the paginator
            //await interativy.SendPaginatedMessage(ctx.Channel, ctx.User, pgs.Paginador, TimeSpan.FromMinutes(5), TimeoutBehaviour.Delete);

            //DiscordMessage msg = await ctx.RespondAsync("pitas é gay?");
            //List<DiscordEmoji> emojis = new List<DiscordEmoji>
            //{
            //    DiscordEmoji.FromUnicode("✅"),
            //    DiscordEmoji.FromUnicode("❌")
            //};
            //ReactionCollectionContext rc = await interativy.CreatePollAsync(msg, emojis, TimeSpan.FromSeconds(10));
            //await msg.ModifyAsync((rc.Reactions[emojis[0]] >= rc.Reactions[emojis[1]]) ? "Sabia" : "Como assim pitas não é gay");
        }
    }
}
