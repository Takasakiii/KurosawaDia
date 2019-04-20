namespace Bot.Modelos
{
    public class ApiConfig
    {
        public string weebToken { private set; get; }
        public uint id { private set; get; }
        public ApiConfig(uint id, string weebToken = null)
        {
            this.weebToken = weebToken;
            this.id = id;
        }

        public void setApiConfig(string weebToken)
        {
            this.weebToken = weebToken;
        }
    }
}
