using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Atributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    [Modulo("Moderação", "⚖")]
    [Description("Este módulo possui coisas para te ajudar a moderar seu servidor.")]
    public class Moderacao
    {
        [Command("limparchat")]
        [Aliases("prune", "clear")]
        [Description("Limpa o chat.\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando.)")]
        [RequirePermissions(Permissions.ManageMessages & Permissions.Administrator)]
        public async Task LimparChat(CommandContext ctx, [Description("Quantidade de mensagens para apagar.")]int quantidade = 10, [Description("Usuário que você deseja que as mensagens sejam apagadas.")][RemainingText]DiscordUser usuario = null)
        {
            if (ctx.Channel.IsPrivate || !PermissionExtension.ValidarPermissoes(ctx, Permissions.ManageMessages))
                throw new Exception();

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
                await ctx.Message.DeleteAsync();
            }
        }

        private enum TipoEliminar
        {
            expulso,
            banido,
            removido
        }

        [Command("kick")]
        [Description("Expulsa um usuário.\n\n(Observação: você precisa da permissão de expulsar membros para poder usar esse comando.)")]
        [RequirePermissions(Permissions.KickMembers & Permissions.Administrator)]
        public async Task Kick(CommandContext ctx, [Description("Usuário que deseja expulsar.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!PermissionExtension.ValidarPermissoes(ctx, Permissions.KickMembers))
                throw new Exception("Sem permissoes");
            await Eliminar(ctx, usuario, motivo, TipoEliminar.expulso);
        }

        [Command("ban")]
        [Description("Bane um usuário.\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando.)")]
        [RequirePermissions(Permissions.BanMembers & Permissions.Administrator)]
        public async Task Ban(CommandContext ctx, [Description("Usuário que deseja banir.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!PermissionExtension.ValidarPermissoes(ctx, Permissions.BanMembers))
                throw new Exception("Sem permissoes");
            await Eliminar(ctx, usuario, motivo, TipoEliminar.banido);
        }

        [Command("softban")]
        [Description("Expulsa um usuário e apaga suas mensagens.\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando.)")]
        [RequirePermissions(Permissions.BanMembers & Permissions.Administrator)]
        public async Task SoftBan(CommandContext ctx, [Description("Usuário que deseja remover.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!PermissionExtension.ValidarPermissoes(ctx, Permissions.BanMembers))
                throw new Exception("Sem permissoes");
            await Eliminar(ctx, usuario, motivo, TipoEliminar.removido);
        }

        private async Task Eliminar(CommandContext ctx, DiscordUser usuario, string motivo, TipoEliminar tipo)
        {
            if (ctx.Channel.IsPrivate)
                throw new Exception();

            IEnumerable<DiscordRole> botRole = (await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id)).Roles;

            if (botRole.Count() == 0)
                throw new Exception();

            IEnumerable<DiscordRole> usuarioRole = (await ctx.Guild.GetMemberAsync(usuario.Id)).Roles;

            int botPermisao = botRole.OrderBy(x => x.Position).Last().Position;
            int usuarioPermisao = 0;
            if (usuarioRole.Count() != 0)
            {
                usuarioPermisao = usuarioRole.OrderBy(x => x.Position).Last().Position;
            }

            if (ctx.Client.CurrentUser == usuario || botPermisao <= usuarioPermisao)
                throw new Exception();

            DiscordMember membro = await ctx.Guild.GetMemberAsync(usuario.Id);
            DiscordDmChannel dm = await membro.CreateDmChannelAsync();

            DiscordEmbedBuilder eb = new DiscordEmbedBuilder
            {
                Title = "**Buuuu buuuu desu waaaa!!!!!**",
                Description = $"Você foi {tipo} do servidor **{ctx.Guild.Name}**.",
                ImageUrl = "https://i.imgur.com/bwifre6.jpg",
                Color = DiscordColor.Black
            };

            if (motivo != "")
            {
                eb.AddField("Motivo: ", motivo);
            }

            eb.AddField("Responsável: ", $"{ctx.User.Username}#{ctx.User.Discriminator}");

            await dm.SendMessageAsync(embed: eb);

            await Task.Delay(1000);

            switch (tipo)
            {
                case TipoEliminar.expulso:
                    await membro.RemoveAsync($"Responsável: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
                    break;
                case TipoEliminar.banido:
                    await membro.BanAsync(7, $"Responsável: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
                    break;
                case TipoEliminar.removido:
                    await membro.BanAsync(7, $"Responsável: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
                    await ctx.Guild.UnbanMemberAsync(membro);
                    break;
                default:
                    break;
            }

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Description = $"Prontinhooo o {usuario.Username}#{usuario.Discriminator} foi {tipo} do servidor 😀",
                Color = DiscordColor.Black
            });
        }
    }
}
