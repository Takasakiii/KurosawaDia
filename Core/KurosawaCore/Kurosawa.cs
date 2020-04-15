using ConfigController.Models;
using DataBaseController;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using KurosawaCore.Configuracoes;
using KurosawaCore.Events;
using KurosawaCore.Extensions;
using KurosawaCore.Models.Abstract;
using KurosawaCore.Singletons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KurosawaCore
{
    public sealed class Kurosawa : IAsyncDisposable
    {
        public delegate void OnLogReceived(LogMessage e);
        public event OnLogReceived OnLog;

        private DiscordClient Cliente;
        private readonly BaseConfig Config;

        public Kurosawa(BaseConfig config, ApiConfig[] apiConfig, DBConfig dbconfig, StatusConfig[] status)
        {
            DependencesSingleton.ApiConfigs = apiConfig;
            new DBCore(dbconfig);
            BotPermissions.IDOwner = config.IdDono;
            Config = config;
            DiscordConfiguration discordConfig = new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            };
            Cliente = new DiscordClient(discordConfig);
            new UserGuildEnter(ref Cliente);
            new UserGuildExit(ref Cliente);
            new MessageReceived(ref Cliente);
            new ReadyEvent(ref Cliente, status);
            Cliente.DebugLogger.LogMessageReceived += DebugLogger_LogMessageReceived;
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
                CustomPrefixPredicate = new PrefixConfig(Config.Prefixo).PegarPrefixo,
            };
            CommandsNextModule comandos = Cliente.UseCommandsNext(configNext);
            comandos.SetHelpFormatter<HelpConfig>();
            comandos.RegisterCommands(typeof(Kurosawa).Assembly);
            foreach (KeyValuePair<string, Command> comando in comandos.RegisteredCommands)
            {
                Cliente.DebugLogger.LogMessage(LogLevel.Debug, "Kurosawa Dia - Handler", $"Comando Registrado: {comando.Key}", DateTime.Now);
            }
            new CommandErrored(ref comandos);
            Cliente.MessageCreated -= comandos.HandleCommandsAsync;
            await Cliente.ConnectAsync();
            await Task.Delay(-1);
        }

        public async ValueTask DisposeAsync()
        {
            if (Cliente != null)
            {
                await Cliente.DisconnectAsync();
                Cliente.Dispose();
                GC.Collect();
            }
        }
    }

}
