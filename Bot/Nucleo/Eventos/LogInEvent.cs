using Bot.Configs.DAO;
using Bot.Configs.Modelos;
using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Nucleo.Eventos
{
    public class LogInEvent
    {
        private readonly DiscordSocketClient client;
        public LogInEvent(DiscordSocketClient client)
        {
            this.client = client;
        }

        public async Task LogIn()
        {
            await client.SetGameAsync("Bom Dia");

            new Thread(async () =>
            {
                List<StatusConfig> status = new StatusDAO().getStatus();
                do
                {
                    for (int i = 0; i < status.Count; i++)
                    {
                        try
                        {
                            switch (status[i].tipo)
                            {
                                case 0:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Playing);
                                    break;
                                case 1:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Streaming);
                                    break;
                                case 2:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Listening);
                                    break;
                                case 3:
                                    await client.SetGameAsync(status[i].status, type: Discord.ActivityType.Watching);
                                    break;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        Thread.Sleep(8000);
                    }
                } while (true);
            }).Start();
        }
    }
}
