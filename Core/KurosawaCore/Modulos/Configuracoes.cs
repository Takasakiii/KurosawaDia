using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Configurações", "⚙")]
    [Description("Em configurações você define preferencias de como agirei em seu servidor.")]
    public class Configuracoes
    {
        [Command("setprefix")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Modifica o meu prefixo")]
        public async Task SetPrefix(CommandContext ctx, [Description("O meu novo prefixo que desejar")]string novoPrefixo)
        {
            if (string.IsNullOrEmpty(novoPrefixo)) 
                throw new Exception();
            DiscordMessage msg = await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Color = DiscordColor.Yellow,
                Title = $"**{ctx.User.Username}**, você quer mudar o prefixo?",
                Description = "Se não, apenas ignore essa mensagem."
            }.Build());
            DiscordEmoji emoji = DiscordEmoji.FromUnicode("✅");
            await msg.CreateReactionAsync(emoji);
            ReactionsController<CommandContext> rc = new ReactionsController<CommandContext>(ctx);
            rc.AddReactionEvent(msg, rc.ConvertToMethodInfo<string>(EmojiModificar), emoji, ctx.User, novoPrefixo);
        }

        private async Task EmojiModificar(CommandContext ctx, string novoprefix)
        {
            await new ServidoresDAO().Atualizar(new Servidores
            {
                ID = ctx.Guild.Id,
                Prefix = novoprefix,
            });
            
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, meu prefixo foi alterado com sucesso para `{novoprefix}`!",
                Color = DiscordColor.Green
            }.Build());
        }
    }
}
