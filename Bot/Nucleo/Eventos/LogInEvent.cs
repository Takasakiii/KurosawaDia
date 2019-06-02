using Discord.WebSocket;
using System;
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

        private readonly Tuple<string, int>[] status =
        {
                Tuple.Create("Flores", 0),
                Tuple.Create("Você se inscrevendo no pewdiepie", 3),
                Tuple.Create("A op de joaninha versão rock", 2),
                Tuple.Create("e passando raiva vendo kuzu no honkai", 3),
                Tuple.Create("os nego me chamando de depresso", 2)
        };

        public async Task LogIn()
        {
            await client.SetGameAsync("Bom Dia");

            new Thread(async () =>
            {
                do
                {
                    for (int i = 0; i < status.Length; i++)
                    {
                        try
                        {
                            switch (status[i].Item2)
                            {
                                case 0:
                                    await client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Playing);
                                    break;
                                case 1:
                                    await client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Streaming);
                                    break;
                                case 2:
                                    await client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Listening);
                                    break;
                                case 3:
                                    await client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Watching);
                                    break;
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        Thread.Sleep(5000);
                    }
                } while (true);
            }).Start();
        }
    }
}
