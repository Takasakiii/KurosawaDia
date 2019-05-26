using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Singletons
{
    public static class SingletonClient
    {
        public static DiscordSocketClient client { get; private set; }

        public static void criarClient()
        {
            client = new DiscordSocketClient();
        }

        public static void setNull()
        {
            client.Dispose();
        }
    }
}
