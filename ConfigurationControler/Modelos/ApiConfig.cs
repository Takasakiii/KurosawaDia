namespace ConfigurationControler.Modelos
{
    public class ApiConfig
    {
        public const uint id = 1;
        public string WeebToken { private set; get; }

        public ApiConfig(string WeebToken)
        {
            this.WeebToken = WeebToken;
        }
    }
}
