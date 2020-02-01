using Bot.Extensions;
using Discord.WebSocket;
using System;

namespace Bot.Singletons
{
    /*
     * Classe responsavel por armazenar o DiscordShardedClient (Objeto principal da Discord.net que representa a Dia(bot) e seus shards) e disponibilizar o mesmo para o projeto inteiro
     */
    public static class SingletonClient
    {
        //Metodo que contem o DiscordShardedClient
        public static DiscordShardedClient client { get; private set; }

        //Metodo permite criar um novo DiscordShardedClient
        public static void criarClient()
        {
            client = new DiscordShardedClient(new DiscordSocketConfig
            {
                AlwaysDownloadUsers = false,
                MessageCacheSize = 50,
                ExclusiveBulkDelete = true,
                TotalShards = 10,
                LogLevel = Discord.LogSeverity.Info
            }) ;

        }


        //Classe responsavel por desalocar o DiscordShardedClient
        public static void setNull()
        {
            client.Dispose();
            client = null;
            GC.Collect();
        }
    }
}
