using ConfigController.Models;
using DataBaseController;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KurosawaCore.Configuracoes;
using KurosawaCore.Extensions;
using KurosawaCore.Modelos;
using KurosawaCore.Modulos;
using KurosawaCore.Singletons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KurosawaCore
{
    public sealed class Kurosawa
    {
        public delegate void OnLogReceived(LogMessage e);
        public event OnLogReceived OnLog;

        private DiscordClient Cliente;
        private readonly BaseConfig Config;
        private MessageCreateEventArgs Message;

        public Kurosawa(BaseConfig config, ApiConfig[] apiConfig, DBConfig dbconfig)
        {
            DependencesSingleton.ApiConfigs = apiConfig;
            new DBCore(dbconfig);
            Config = config;
            DiscordConfiguration discordConfig = new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            };
            Cliente = new DiscordClient(discordConfig);
            Cliente.DebugLogger.LogMessageReceived += DebugLogger_LogMessageReceived;
            Cliente.MessageCreated += Cliente_MessageCreated;
        }

        private Task Cliente_MessageCreated(MessageCreateEventArgs e)
        {
            Message = e;
            return Task.CompletedTask;
        }

        private void DebugLogger_LogMessageReceived(object sender, DebugLogMessageEventArgs e)
        {
            OnLog?.Invoke(new LogMessage(e));
        }

        public async Task Iniciar()
        {
            CommandsNextConfiguration configNext = new CommandsNextConfiguration
            {
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                CustomPrefixPredicate = new PrefixConfig().PegarPrefixo
            };
            CommandsNextModule comandos = Cliente.UseCommandsNext(configNext);
            comandos.SetHelpFormatter<HelpConfig>();
            comandos.RegisterCommands(typeof(Kurosawa).Assembly);
            foreach (KeyValuePair<string, Command> comando in comandos.RegisteredCommands)
            {
                Cliente.DebugLogger.LogMessage(LogLevel.Debug, "Kurosawa Dia - Handler", $"Comando Registrado: {comando.Key}", DateTime.Now);
            }
            comandos.CommandErrored += Comandos_CommandErrored;
            await Cliente.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task Comandos_CommandErrored(CommandErrorEventArgs e)
        {
            try
            {
                ReactionsController<CommandContext> controller = new ReactionsController<CommandContext>(e.Context);
                if (e.Exception is CommandNotFoundException)
                {
                    DiscordEmoji emoji = DiscordEmoji.FromUnicode("❓");
                    await e.Context.Message.CreateReactionAsync(emoji);
                    controller.AddReactionEvent(e.Context.Message, controller.ConvertToMethodInfo(CallHelpNofing), emoji, e.Context.User);
                }
                else
                {
                    DiscordEmoji emoji = DiscordEmoji.FromUnicode("❌");
                    await Message.Message.CreateReactionAsync(emoji);
                    controller.AddReactionEvent(e.Context.Message, controller.ConvertToMethodInfo<string>(CallHelp), emoji, e.Context.User, e.Command.Name);
                    Cliente.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", e.Exception.Message, DateTime.Now);
                }
            }
            catch(Exception ex)
            {
                Cliente.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", ex.Message, DateTime.Now);
            }
        }

        private async Task CallHelp(CommandContext ctx, string arg)       
        {
            await ctx.Client.GetCommandsNext().DefaultHelpAsync(ctx, arg);
        }
        private async Task CallHelpNofing(CommandContext ctx)
        {
            await new Ajuda().AjudaCmd(ctx);
        }
    }

}
