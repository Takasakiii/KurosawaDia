using DSharpPlus.CommandsNext;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class CommandExecuted
    {
        internal CommandExecuted(ref CommandsNextModule next)
        {
            next.CommandExecuted += Next_CommandExecuted;
        }

        private Task Next_CommandExecuted(CommandExecutionEventArgs e)
        {
            Console.WriteLine($"[{e.Command.Name}] [{e.Context.Message.Author.Username}#{e.Context.Message.Author.Discriminator}] [{e.Context.Message.Content ?? "Arquivo"}]");
            return Task.CompletedTask;
        }
    }
}
