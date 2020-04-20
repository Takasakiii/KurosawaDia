using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using KurosawaCore.Extensions;
using KurosawaCore.Modulos;
using System;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class CommandErrored
    {
        internal CommandErrored(ref CommandsNextModule next)
        {
            next.CommandErrored += Comandos_CommandErrored;
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
                    await e.Context.Message.CreateReactionAsync(emoji);
                    controller.AddReactionEvent(e.Context.Message, controller.ConvertToMethodInfo<string>(CallHelp), emoji, e.Context.User, e.Command.QualifiedName);
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", e.Exception.Message, DateTime.Now);
                }
            }
            catch (Exception ex)
            {
                e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", ex.Message, DateTime.Now);
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
