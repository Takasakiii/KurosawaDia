using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using KurosawaCore.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class MessageReceived
    {

        internal MessageReceived(ref DiscordClient client)
        {
            client.MessageCreated += Client_MessageCreated;
        }

        private async Task Client_MessageCreated(MessageCreateEventArgs e)
        {
            if(e.Message.Content == $"<@!{e.Client.CurrentUser.Id}>" || e.Message.Content == $"<@{e.Client.CurrentUser.Id}>")
            {
                string prefix = await PrefixExtension.GetPrefix(e.Message);
                await e.Channel.SendMessageAsync(embed: new DiscordEmbedBuilder
                {
                    Color = DiscordColor.PhthaloBlue,
                    Title = $"Oii {e.Author.Username}, meu prefixo é `{prefix}`, se quiser ver os meus comandos é so usar `{prefix}help`!"
                });
            }
        }
    }
}
