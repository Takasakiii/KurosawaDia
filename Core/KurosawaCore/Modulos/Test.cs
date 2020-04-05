using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Group("Test")]
    [Hidden]
    [Description("Não sei como você chegou aqui mas esse lugar so tem funções tediantes de teste, a menos que seja um fanatico dos testes tomar um sorvete com a Mari é mais divertido que explorar isso!")]
    public class Test
    {
        [Hidden]
        [Command("ping")]
        public async Task Ping(CommandContext ctx, [Description("aaaaaaaa")]bool a = false)
        {
            //DiscordEmoji emoji = DiscordEmoji.FromUnicode("❓");
            //await ctx.Message.CreateReactionAsync(emoji);
            //ReactionsController<CommandContext> controler = new ReactionsController<CommandContext>(ctx);
            //controler.AddReactionEvent(ctx.Message, TestReceiv, emoji, ctx.User);
        }

        private async Task TestReceiv(CommandContext ctx)
        {
            await ctx.RespondAsync("PitasGay");
        }
    }
}
