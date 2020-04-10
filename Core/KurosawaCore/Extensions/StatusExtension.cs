using ConfigController.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KurosawaCore.Extensions
{
    internal sealed class StatusExtension
    {
        private StatusConfig[] Status;
        private DiscordClient Client;
        internal StatusExtension(DiscordClient client, StatusConfig[] status)
        {
            Client = client;
            Client.Ready += Client_Ready;
            Status = status;
        }

        private async Task Client_Ready(ReadyEventArgs e)
        {
            if(Status != null && Status.Length > 0)
                while (true)
                    foreach (StatusConfig status in Status)
                    {
                        DiscordGame game = new DiscordGame
                        {
                            Name = status.StatusJogo
                        };
                        await Client.UpdateStatusAsync(game);
                        await Task.Delay(10000);
                    }
                    
        }
    }
}
