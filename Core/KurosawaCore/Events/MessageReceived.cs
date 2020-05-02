using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KurosawaCore.Extensions;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class MessageReceived
    {
        private DiscordClient Cliente;
        internal MessageReceived(ref DiscordClient client)
        {
            client.MessageCreated += Client_MessageCreated;
            Cliente = client;
        }

        private Task Client_MessageCreated(MessageCreateEventArgs e)
        {
            new Thread(Session).Start(e);
            return Task.CompletedTask;
        }


        private async void Session(object comcertezanehoe)
        {
            MessageCreateEventArgs e = (MessageCreateEventArgs)comcertezanehoe;
            try
            {
                if (e.Message.Content == $"<@!{e.Client.CurrentUser.Id}>" || e.Message.Content == $"<@{e.Client.CurrentUser.Id}>")
                {
                    string prefix = await PrefixExtension.GetPrefix(e.Message);
                    await e.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
                    {
                        Color = DiscordColor.PhthaloBlue,
                        Title = $"Oii {e.Author.Username}, meu prefixo é `{prefix}`, se quiser ver os meus comandos é so usar `{prefix}help`!"
                    });
                }
                else
                {
                    CommandsNextModule comandos = Cliente.GetCommandsNext();
                    await comandos.HandleCommandsAsync(e);
                }
            }
            catch (Exception ex)
            {
                e.Client.DebugLogger.LogMessage(LogLevel.Info, "Kurosawa Dia - Event", ex.Message, DateTime.Now);
            }
        }
    }
}
