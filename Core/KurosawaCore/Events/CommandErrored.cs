using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
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
                DiscordEmoji emoji;
                bool rota = false;
                //ReactionsController<CommandContext> controller = new ReactionsController<CommandContext>(e.Context);

                if (e.Exception is CommandNotFoundException)
                {
                    emoji = DiscordEmoji.FromUnicode("❓");
                    await e.Context.Message.CreateReactionAsync(emoji);
                    rota = true;
                    //controller.AddReactionEvent(e.Context.Message, controller.ConvertToMethodInfo(CallHelpNofing), emoji, e.Context.User);
                }
                else
                {
                    emoji = DiscordEmoji.FromUnicode("❌");
                    await e.Context.Message.CreateReactionAsync(emoji);
                    //controller.AddReactionEvent(e.Context.Message, controller.ConvertToMethodInfo<string>(CallHelp), emoji, e.Context.User, e.Command.QualifiedName);
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", e.Exception.ToString(), DateTime.Now);
                }

                ReactionContext recebimento = await e.Context.Client.GetInteractivityModule().WaitForMessageReactionAsync(predicate: x => x == emoji, user: e.Context.User, message: e.Context.Message);

                if (recebimento != null)
                {
                    if (rota)
                    {
                        await new Ajuda().AjudaCmd(e.Context);
                    }
                    else
                    {
                        await e.Context.Client.GetCommandsNext().DefaultHelpAsync(e.Context, e.Command.QualifiedName);
                    }
                }
            }
            catch (Exception ex)
            {
                e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, "Kurosawa Dia - Handler", ex.ToString(), DateTime.Now);
            }
        }

    }
}
