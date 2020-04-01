using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Group("Test")]
    [Hidden]
    [Description("Não sei como você chegou aqui mas esse lugar so tem funções tediantes de teste, a menos que seja um fanatico dos testes tomar um sorvete com a Mari é mais divertido que explorar isso!")]
    public class Test
    {
        [Command("ping")]
        public async Task Ping(CommandContext ctx, [Description("aaaaaaaa")]bool a = false)
        {
            await ctx.RespondAsync($"👋, {ctx.User.Mention}!");
        }
    }
}
