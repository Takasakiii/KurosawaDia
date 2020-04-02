using ConfigController.Models;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using KurosawaCore.Configuracoes;
using KurosawaCore.Singletons;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KurosawaCore
{
    public sealed class Kurosawa
    {
        private DiscordClient Cliente;
        private readonly BaseConfig Config;
        private CommandsNextModule Comandos;

        public Kurosawa(BaseConfig config, ApiConfig[] apiConfig)
        {
            DependencesSingleton.ApiConfigs = apiConfig;
            Config = config;
            DiscordConfiguration discordConfig = new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                UseInternalLogHandler = true,
                LogLevel = LogLevel.Debug
            };
            Cliente = new DiscordClient(discordConfig);

        }

        public async Task Iniciar()
        {
            CommandsNextConfiguration configNext = new CommandsNextConfiguration
            {
                StringPrefix = Config.Prefixo,
                EnableDefaultHelp = false
            };
            Comandos = Cliente.UseCommandsNext(configNext);
            Comandos.SetHelpFormatter<HelpConfig>();
            Comandos.RegisterCommands(typeof(Kurosawa).Assembly);
            foreach (KeyValuePair<string, Command> comando in Comandos.RegisteredCommands)
            {
                Cliente.DebugLogger.LogMessage(LogLevel.Debug, "Handler", $"Comando Registrado: {comando.Key}", DateTime.Now);
            }
            Comandos.CommandErrored += Comandos_CommandErrored;
            await Cliente.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task Comandos_CommandErrored(CommandErrorEventArgs e)
        {
            Cliente.DebugLogger.LogMessage(LogLevel.Error, "Handler", e.Exception.Message, DateTime.Now);
            return Task.CompletedTask;
        }
    }

}
