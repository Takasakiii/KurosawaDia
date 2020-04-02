using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    public class Moderacao
    {
        [Command("limparchat")]
        [Aliases("prune")]
        [Description("Limpar todo o chat")]
        [RequirePermissions(Permissions.ManageMessages & Permissions.Administrator)]
        [RequireUserPermissions(Permissions.ManageMessages & Permissions.Administrator)]
        public async Task LimparChat(CommandContext ctx, [Description("Quantidade de mensagens")]int quantidade = 10, [Description("Usuario que deseja apagar as mensagens")][RemainingText]DiscordUser usuario = null)
        {
            if (!ctx.Channel.IsPrivate)
            {
                if (usuario == null)
                {
                    IReadOnlyList<DiscordMessage> mensagens = await ctx.Channel.GetMessagesAsync(quantidade);
                    await ctx.Channel.DeleteMessagesAsync(mensagens);
                }
                else
                {
                    List<DiscordMessage> mensagens = new List<DiscordMessage>();
                    ulong referencia = (await ctx.Channel.GetMessageAsync(ctx.Channel.LastMessageId)).Id;

                    int vezes = 0;

                    do
                    {
                        mensagens.AddRange((await ctx.Channel.GetMessagesAsync(before: referencia)).Where(x => x.Author == usuario));
                        referencia = mensagens.Last().Id;
                        vezes++;
                        if (vezes >= 3)
                        {
                            break;
                        }
                    } while (mensagens.Count < quantidade);

                    await ctx.Channel.DeleteMessagesAsync(mensagens.GetRange(0, (quantidade > mensagens.Count) ? mensagens.Count : quantidade));
                }
            }
        }
    }
}
