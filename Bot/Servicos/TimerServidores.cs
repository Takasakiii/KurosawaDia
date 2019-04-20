using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Bot.Servicos
{
    public class TimerServidores
    {
        public async Task SetServidor(DiscordSocketClient client)
        {

        }

        public void ThreadServidoresList(DiscordSocketClient client)
        {
            while (true)
            {
                for(int i = 0; i < client.Guilds.Count; i++)
                {
                    SetServidor(client).Wait();
                    Thread.Sleep(5000);
                }
            }
        }

        public void SetServidores(DiscordSocketClient client)
        {
            Thread servidoresEngine = new Thread(() => ThreadServidoresList(client));
            servidoresEngine.Start();
        }
    }
}
