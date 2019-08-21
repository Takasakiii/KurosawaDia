using Discord.WebSocket;

namespace Bot.Singletons
{
    /*
     * Classe responsavel por armazenar o DiscordSocketClient (Objeto principal da Discord.net que representa a Dia(bot)) e disponibilizar o mesmo para o projeto inteiro
     */
    public static class SingletonClient
    {
        //Metodo que contem o DiscordSocketClient
        public static DiscordSocketClient client { get; private set; }

        //Metodo permite criar um novo DiscordSocketClient
        public static void criarClient()
        {
            client = new DiscordSocketClient();
        }


        //Classe responsavel por desalocar o DiscordSocketClient
        public static void setNull()
        {
            client.Dispose();
            client = null;
        }
    }
}
