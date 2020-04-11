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
        [Aliases("prefix")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Modifica o meu prefixo.")]
        public async Task SetPrefix(CommandContext ctx, [Description("O meu novo prefixo que desejar")]string novoPrefixo)
        {
            if (string.IsNullOrEmpty(novoPrefixo) || ctx.Channel.IsPrivate) 
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
            rc.AddReactionEvent(msg, rc.ConvertToMethodInfo<Tuple<DiscordMessage, string>>(EmojiModificar), emoji, ctx.User, Tuple.Create(msg, novoPrefixo));
        }

        private async Task EmojiModificar(CommandContext ctx, Tuple<DiscordMessage, string> args)
        {
            await new ServidoresDAO().Atualizar(new Servidores
            {
                ID = ctx.Guild.Id,
                Prefix = args.Item2,
            });

            await args.Item1.DeleteAsync();

            await ctx.RespondAsync(embed: new DiscordEmbedBuilder
            {
                Title = $"{ctx.User.Username}, meu prefixo foi alterado com sucesso para `{args.Item2}`!",
                Color = DiscordColor.Green
            }.Build());
        }

        [Command("bemvindo")]
        [Aliases("welcome", "entrada", "greetmsg")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Configura a mensagem de entrada do servidor.")]
        public async Task SetBemVindo(CommandContext ctx, [Description("Texto ou Embed que deseja colocar como mensagem de bem-vindo.")][RemainingText] string message)
        {
            if (string.IsNullOrEmpty(message) || ctx.Channel.IsPrivate)
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
                Title = $"{ctx.User.Username}, a mensagem de entrada do servidor foi cadastrada com sucesso!",
                Color = DiscordColor.Green
            });
        }

        [Command("setcanalbemvindo")]
        [Aliases("setcanalentrada", "setcanalwelcome", "greet")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Selecionar canal de bem vindo.\nSe for usado o comando novamente sera desativado a mensagem.")]
        public async Task SetCanalBemVindo(CommandContext ctx, [Description("Canal de bem vindo")]DiscordChannel canal = null)
        {
            canal ??= ctx.Channel;

            if (canal.IsPrivate || canal.Type != ChannelType.Text || canal.GuildId != ctx.Guild.Id)
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
                Title = "O canal de bem vindo foi selecionado com sucesso",
                Color = DiscordColor.Green
            });
        }

        [Command("saida")]
        [Aliases("leave", "byemsg")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Configura a mensagem de saida do servidor.")]
        public async Task SetSaida(CommandContext ctx, [Description("Texto ou Embed que deseja colocar como mensagem de saida.")][RemainingText] string message)
        {
            if (string.IsNullOrEmpty(message) || ctx.Channel.IsPrivate)
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
                Title = $"{ctx.User.Username}, a mensagem de saída do servidor foi cadastrada com sucesso!",
                Color = DiscordColor.Green
            });
        }

        [Command("setcanalsaida")]
        [Aliases("setcanalleave", "bye")]
        [RequireUserPermissions(Permissions.Administrator & Permissions.ManageGuild)]
        [Description("Selecionar canal de saida.\nSe for usado o comando novamente sera desativado a mensagem.")]
        public async Task SetCanalSaida(CommandContext ctx, [Description("Canal de saida")]DiscordChannel canal = null)
        {
            canal ??= ctx.Channel;

            if (canal.IsPrivate || canal.Type != ChannelType.Text || canal.GuildId != ctx.Guild.Id)
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
                Title = "O canal de saida foi selecionado com sucesso",
                Color = DiscordColor.Green
            });
        }
    }
}
