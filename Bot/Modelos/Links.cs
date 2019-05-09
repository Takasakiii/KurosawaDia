namespace Bot.Modelos
{
    public class Links
    {
        public string url { private set; get; }
        public string tipo { private set; get; }

        public Links(string url, string tipo)
        {
            this.url = url; //volto a falar q isso eh uma constante ou enumerate
            this.tipo = tipo;
        }
    }
}
