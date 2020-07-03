using DataBaseController.Abstractions;
using DataBaseController.DAOs;
using DataBaseController.Modelos;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
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
        [Aliases("prefix")]
        [Description("Modifica o meu prefixo em um servidor.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)")]
        public async Task SetPrefix(CommandContext ctx, [Description("O meu novo prefixo no servidor.")]string novoPrefixo)
        {
            if (string.IsNullOrEmpty(novoPrefixo) || ctx.Channel.IsPrivate || !ctx.HasPermissions(Permissions.ManageGuild))
                throw new Exception();

            DiscordMessage msg = await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Color = DiscordColor.Yellow,
                Title = $"**{ctx.User.Username}**, você quer mudar o prefixo?",
                Description = "Se não, apenas ignore essa mensagem."
            }.Build());
            DiscordEmoji emoji = DiscordEmoji.FromUnicode("✅");
            await msg.CreateReactionAsync(emoji);
            ReactionContext contexto = await ctx.Client.GetInteractivityModule().WaitForMessageReactionAsync(predicate: x => x == emoji, message: msg, user: ctx.User);
            if (contexto != null)
            {
                await EmojiModificar(ctx, msg, novoPrefixo);
            }
        }

        private async Task EmojiModificar(CommandContext ctx, DiscordMessage msg, string args)
        {
            await new ServidoresDAO().Atualizar(new Servidores
            {
                ID = ctx.Guild.Id,
                Prefix = args,
            });

            await msg.DeleteAsync();

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, meu prefixo foi alterado com sucesso para `{args}`!",
                Color = DiscordColor.Green
            }.Build());
        }

        [Command("bemvindo")]
        [Aliases("welcome", "entrada", "greetmsg")]
        [Description("Define a mensagem de bem-vindo do servidor.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)")]
        public async Task SetBemVindo(CommandContext ctx, [Description("Texto ou embed que deseja definir como mensagem de bem-vindo.")][RemainingText] string message)
        {
            if (string.IsNullOrEmpty(message) || ctx.Channel.IsPrivate || !ctx.HasPermissions(Permissions.ManageGuild))
                throw new Exception();

            await new ConfiguracoesServidoresDAO().Add(new ConfiguracoesServidores
            {
                Configuracoes = TiposConfiguracoes.BemVindoMsg,
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id
                },
                Value = message
            });
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, a mensagem de bem-vindo do servidor foi definida com sucesso!",
                Color = DiscordColor.Green
            });
        }

        [Command("canalbemvindo")]
        [Aliases("canalentrada", "canalwelcome", "greet")]
        [Description("Define o canal de bem vindo.\nSe usado novamente, a mensagem será desativada.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)")]
        public async Task SetCanalBemVindo(CommandContext ctx, [Description("Canal onde será enviada a mensagem de bem-vindo.")]DiscordChannel canal = null)
        {
            canal ??= ctx.Channel;

            if (canal.IsPrivate || canal.Type != ChannelType.Text || canal.GuildId != ctx.Guild.Id || !ctx.HasPermissions(Permissions.ManageGuild))
                throw new Exception();

            await new CanaisDAO().Adicionar(new Canais
            {
                ID = canal.Id,
                Nome = canal.Name,
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id,
                },
                TipoCanal = TiposCanais.BemVindo
            });

            await ctx.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, o canal de bem-vindo foi definido com sucesso!",
                Color = DiscordColor.Green
            });
        }

        [Command("saida")]
        [Aliases("leave", "byemsg", "saída")]
        [Description("Define a mensagem de saída do servidor.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)")]
        public async Task SetSaida(CommandContext ctx, [Description("Texto ou embed que deseja definir como mensagem de saída.")][RemainingText] string message)
        {
            if (string.IsNullOrEmpty(message) || ctx.Channel.IsPrivate || !ctx.HasPermissions(Permissions.ManageGuild))
                throw new Exception();

            await new ConfiguracoesServidoresDAO().Add(new ConfiguracoesServidores
            {
                Configuracoes = TiposConfiguracoes.SaidaMsg,
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id
                },
                Value = message
            });
            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, a mensagem de saída do servidor foi definida com sucesso!",
                Color = DiscordColor.Green
            });
        }

        [Command("canalsaida")]
        [Aliases("canalleave", "canalbye", "bye", "canalsaída")]
        [Description("Define o canal de saída.\nSe usado novamente, a mensagem será desativada.\n\n(Observação: você precisa da permissão de administrador ou da permissão de gerenciar servidor para poder usar esse comando.)")]
        public async Task SetCanalSaida(CommandContext ctx, [Description("Canal onde será enviada a mensagem de bem-vindo.")]DiscordChannel canal = null)
        {
            canal ??= ctx.Channel;
            if (canal.IsPrivate || canal.Type != ChannelType.Text || canal.GuildId != ctx.Guild.Id || !ctx.HasPermissions(Permissions.ManageGuild))
                throw new Exception();

            await new CanaisDAO().Adicionar(new Canais
            {
                ID = canal.Id,
                Nome = canal.Name,
                Servidor = new Servidores
                {
                    ID = ctx.Guild.Id,
                },
                TipoCanal = TiposCanais.Sair
            });

            await ctx.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, o canal de saída foi definido com sucesso!",
                Color = DiscordColor.Green
            });
        }
    }
}
