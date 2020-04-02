using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KurosawaCore.Modulos
{
    public class Moderacao
    {
        [Command("limparchat")]
        [Aliases("prune")]
        [Description("Limpar o chat")]
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

        private enum TipoEliminar
        {
            expulso,
            banido,
            removido
        }

        [Command("kick")]
        [Description("Expulsar o usuario")]
        [RequirePermissions(Permissions.KickMembers & Permissions.Administrator)]
        [RequireUserPermissions(Permissions.KickMembers & Permissions.Administrator)]
        public async Task Kick(CommandContext ctx, [Description("Usuario que deseja expulsar")]DiscordUser usuario, [Description("Motivo")][RemainingText]string motivo)
        {
            await Eliminar(ctx, usuario, motivo, TipoEliminar.expulso);
        }

        [Command("ban")]
        [Description("Banir o usuario")]
        [RequirePermissions(Permissions.BanMembers & Permissions.Administrator)]
        [RequireUserPermissions(Permissions.BanMembers & Permissions.Administrator)]
        public async Task Ban(CommandContext ctx, [Description("Usuario que deseja banir")]DiscordUser usuario, [Description("Motivo")][RemainingText]string motivo)
        {
            await Eliminar(ctx, usuario, motivo, TipoEliminar.banido);
        }

        [Command("softban")]
        [Description("Remover o usuario")]
        [RequirePermissions(Permissions.BanMembers & Permissions.Administrator)]
        [RequireUserPermissions(Permissions.BanMembers & Permissions.Administrator)]
        public async Task SoftBan(CommandContext ctx, [Description("Usuario que deseja remover")]DiscordUser usuario, [Description("Motivo")][RemainingText]string motivo)
        {
            await Eliminar(ctx, usuario, motivo, TipoEliminar.removido);
        }

        private async Task Eliminar(CommandContext ctx, DiscordUser usuario, string motivo, TipoEliminar tipo)
        {
            if (!ctx.Channel.IsPrivate)
            {
                IEnumerable<DiscordRole> botRole = (await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id)).Roles;
                IEnumerable<DiscordRole> usuarioRole = (await ctx.Guild.GetMemberAsync(usuario.Id)).Roles;

                if (botRole.Count() != 0)
                {
                    int botPermisao = botRole.OrderBy(x => x.Position).Last().Position;
                    int usuarioPermisao = 0;
                    if (usuarioRole.Count() != 0)
                    {
                        usuarioPermisao = usuarioRole.OrderBy(x => x.Position).Last().Position;
                    }

                    if (!(ctx.Client.CurrentUser == usuario) && !(botPermisao <= usuarioPermisao))
                    {
                        DiscordMember membro = await ctx.Guild.GetMemberAsync(usuario.Id);
                        DiscordDmChannel dm = await membro.CreateDmChannelAsync();

                        DiscordEmbedBuilder eb = new DiscordEmbedBuilder
                        {
                            Title = "**Buuuu buuuu desu waaaa!!!!!**",
                            Description = $"Você foi {tipo} do servidor **{ctx.Guild.Name}**",
                            ImageUrl = "https://i.imgur.com/bwifre6.jpg",
                            Color = DiscordColor.Black
                        };

                        if (motivo != "")
                        {
                            eb.AddField("Motivo: ", motivo);
                        }

                        eb.AddField("Responsavel: ", $"{ctx.User.Username}#{ctx.User.Discriminator}");

                        await dm.SendMessageAsync(embed: eb);

                        await Task.Delay(1000);

                        switch (tipo)
                        {
                            case TipoEliminar.expulso:
                                await membro.RemoveAsync($"Responsavel: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
                                break;
                            case TipoEliminar.banido:
                                await membro.BanAsync(reason: $"Responsavel: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
                                break;
                            case TipoEliminar.removido:
                                await membro.BanAsync(reason: $"Responsavel: {ctx.User.Username}#{ctx.User.Discriminator} | Motivo: {motivo}");
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
        }
    }
}
