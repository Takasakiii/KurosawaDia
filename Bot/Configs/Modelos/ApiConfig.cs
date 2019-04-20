namespace Bot.Modelos
{
    public class ApiConfig
    {
        public string weebToken { private set; get; }
        public uint id { private set; get; }
        public ApiConfig(string weebToken, uint id)
        {
            this.weebToken = weebToken;
            this.id = id;
        }

        public ApiConfig(uint id)
        {
            this.id = id;
        }
        public void setApiConfig(string weebToken)
        {
            this.weebToken = weebToken;
        }
    }
}
