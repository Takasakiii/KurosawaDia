using ConfigController.Models;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KurosawaCore.Events
{
    internal sealed class ReadyEvent
    {
        private StatusConfig[] Status;
        private DiscordClient Cliente;
        private Thread StatusThread;
        internal ReadyEvent(ref DiscordClient cliente, StatusConfig[] status)
        {
            cliente.Ready += Cliente_Ready;
            Cliente = cliente;
            Status = status;
            StatusThread = new Thread(Read);
        }

        private async Task Cliente_Ready(ReadyEventArgs e)
        {
            await Task.Yield();
            StatusThread.Start();
        }

        private async void Read()
        {
            try
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
                            Thread.Sleep(10000);
                        }
            }
            catch (Exception ex)
            {
                Cliente.DebugLogger.LogMessage(LogLevel.Info, "Kurosawa Dia - Event", ex.Message, DateTime.Now);
            }
        }
    }
}
