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
        [Description("Limpa as mensagens de até 13 dias atras.\n\n(Observação: você precisa da permissão de gerenciar mensagens para poder usar esse comando.)")]
        public async Task LimparChat(CommandContext ctx, [Description("Quantidade de mensagens para apagar.")]int quantidade = 10, [Description("Usuário que você deseja que as mensagens sejam apagadas.")][RemainingText]DiscordUser usuario = null)
        {
            if (ctx.Channel.IsPrivate || !ctx.HasPermissions(Permissions.ManageMessages) || quantidade > 1000)
                throw new Exception();
            if (usuario == null)
            {
                do
                {
                    IReadOnlyList<DiscordMessage> mensagens = await ctx.Channel.GetMessagesAsync((quantidade < 75) ? quantidade : 75);
                    quantidade -= 75;
                    if(mensagens.Count > 0)
                    {
                        await ctx.Channel.DeleteMessagesAsync(mensagens);
                    }
                    await Task.Delay(1000);
                } while (quantidade > 0);
            }
            else
            {
                DiscordMessage referencia = await ctx.Channel.GetMessageAsync(ctx.Channel.LastMessageId);
                int vezes = 0;

                do
                {
                    
                    DiscordMessage[] msgsnaotratadas = (await ctx.Channel.GetMessagesAsync(before: referencia.Id)).ToArray();
                    IEnumerable<DiscordMessage> msgs = msgsnaotratadas[..^1]
                        .Where(x => x.Author == usuario && x.Id != referencia.Id && x.Timestamp.CompareTo(DateTimeOffset.UtcNow.AddDays(-13)) > 0);
                    if (referencia.Author.Id == usuario.Id)
                    {
                        await referencia.DeleteAsync();
                    }
                    referencia = msgsnaotratadas.Last();
                    vezes++;
                    
                    if (msgs.Count() > 0)
                    {
                        if(msgs.Count() > quantidade)
                        {
                            List<DiscordMessage> temp = new List<DiscordMessage>();
                            for(int i = 0; i < quantidade; i++)
                            {
                                temp.Add(msgs.ElementAt(i));
                            }
                            msgs = temp;
                        }
                        quantidade -= msgs.Count();
                        await ctx.Channel.DeleteMessagesAsync(msgs);
                    }
                    if (vezes >= 3)
                    {
                        break;
                    }
                    await Task.Delay(1500);
                } while (quantidade > 0);              
               
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
        public async Task Kick(CommandContext ctx, [Description("Usuário que deseja expulsar.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!ctx.HasPermissions(Permissions.KickMembers))
                throw new Exception("Sem permissoes");
            await Eliminar(ctx, usuario, motivo, TipoEliminar.expulso);
        }

        [Command("ban")]
        [Description("Bane um usuário.\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando.)")]
        public async Task Ban(CommandContext ctx, [Description("Usuário que deseja banir.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!ctx.HasPermissions(Permissions.BanMembers))
                throw new Exception("Sem permissoes");
            await Eliminar(ctx, usuario, motivo, TipoEliminar.banido);
        }

        [Command("softban")]
        [Description("Expulsa um usuário e apaga suas mensagens.\n\n(Observação: você precisa da permissão de banir membros para poder usar esse comando.)")]
        public async Task SoftBan(CommandContext ctx, [Description("Usuário que deseja remover.")]DiscordUser usuario, [Description("Motivo da punição.")][RemainingText]string motivo)
        {
            if (!ctx.HasPermissions(Permissions.BanMembers))
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
