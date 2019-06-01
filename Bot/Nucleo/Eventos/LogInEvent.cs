using Discord.WebSocket;
using System;
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

        public Task LogIn()
        {
            List<Tuple<string, int>> status = new List<Tuple<string, int>>();

            status.Add(Tuple.Create("1", 0));
            status.Add(Tuple.Create("2", 1));
            status.Add(Tuple.Create("3", 2));
            status.Add(Tuple.Create("4", 3));

            new Thread(() =>
            {
                do
                {
                    for (int i = 0; i < status.Count; i++)
                    {
                        try
                        {
                            switch (status[i].Item2)
                            {
                                case 0:
                                    client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Playing);
                                    break;
                                case 1:
                                    client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Streaming);
                                    break;
                                case 2:
                                    client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Listening);
                                    break;
                                case 3:
                                    client.SetGameAsync(status[i].Item1, type: Discord.ActivityType.Watching);
                                    break;
                            }
                        }
                        catch
                        {

                        }
                        Thread.Sleep(10000);
                    }
                } while (true);
            }).Start();

            return null;
        }
    }
}
