namespace Bot.Modelos
{
    public class Links
    {
        public string url { private set; get; }
        public string tipo { private set; get; }

        public Links(string url, string tipo)
        {
            this.url = url;
            this.tipo = tipo;
        }
    }
}
