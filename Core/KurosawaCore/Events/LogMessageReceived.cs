using DSharpPlus;
using DSharpPlus.EventArgs;
using KurosawaCore.Models.Abstract;
using System;

namespace KurosawaCore.Events
{
    internal sealed class LogMessageReceived
    {
        private Action<LogMessage> Acao;
        internal LogMessageReceived(ref DiscordClient cliente, Action<LogMessage> acao)
        {
            cliente.DebugLogger.LogMessageReceived += DebugLogger_LogMessageReceived;
            Acao = acao;
        }

        private void DebugLogger_LogMessageReceived(object sender, DebugLogMessageEventArgs e)
        {
            Acao.Invoke(new LogMessage(e));
        }

    }
}
