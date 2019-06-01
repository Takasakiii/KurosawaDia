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
                Tuple.Create("flores", 0),
                Tuple.Create("você se inscrevendo no pewdiepie", 3),
                Tuple.Create("você xingando o criador do bot", 2),
                Tuple.Create("minha vida fora", 0)
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
                        Thread.Sleep(10000);
                    }
                } while (true);
            }).Start();
        }
    }
}
