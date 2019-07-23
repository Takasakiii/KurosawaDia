namespace ConfigurationControler.Modelos
{
    public class ApiConfig
    {
        public const uint id = 1;
        public string WeebToken { private set; get; }

        public string dblToken { private set; get; }

        public bool atualizarDbl { private set; get; }

        public ApiConfig(string WeebToken, string dblToken, bool atualizarDbl)
        {
            this.WeebToken = WeebToken;
            this.dblToken = dblToken;
            this.atualizarDbl = atualizarDbl;
        }
    }
}
