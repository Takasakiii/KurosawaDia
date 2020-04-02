using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    public class Weeb
    {
        [Command("hug")]
        [Aliases("abraço", "abraco")]
        [Description("Envia um abraço ao seu amigo XD")]
        public async Task Hug(CommandContext ctx, [Description("Usuario que deseja abraçar")][RemainingText]DiscordUser usuario = null)
        {
            await ctx.RespondAsync(embed: await new WeebExtension
            {
                SelfMsg = "está se abraçando",
                UsuarioDestino = usuario
            }.GetWeeb(ctx.User,"hug", "está abraçando"));
        }
    }
}
