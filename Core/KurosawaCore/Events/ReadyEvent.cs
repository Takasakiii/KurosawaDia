using ConfigController.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class ReadyEvent
    {
        private StatusConfig[] Status;
        private DiscordClient Cliente;
        internal ReadyEvent(ref DiscordClient cliente, StatusConfig[] status)
        {
            cliente.Ready += Cliente_Ready;
            Cliente = cliente;
            Status = status;
        }

        private Task Cliente_Ready(ReadyEventArgs e)
        {
            new Thread(Read).Start();
            return Task.CompletedTask;
        }

        private async void Read()
        {
            if (Status != null && Status.Length > 0)
                while (true)
                    foreach (StatusConfig status in Status)
                    {
                        DiscordGame game = new DiscordGame
                        {
                            Name = status.StatusJogo
                        };
                        await Cliente.UpdateStatusAsync(game);
                        await Task.Delay(10000);
                    }
        }
    }
}
