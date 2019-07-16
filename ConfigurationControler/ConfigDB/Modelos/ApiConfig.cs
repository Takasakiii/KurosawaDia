namespace ConfigurationControler.ConfigDB.Modelos
{
    public class ApiConfig
    {
        public string weebToken { private set; get; }
        public uint id { private set; get; }

        public ApiConfig(uint id, string weebToken = null)
        {
            this.id = id;
            this.weebToken = weebToken;
        }

        public void SetApiConfig(string weebToken)
        {
            this.weebToken = weebToken;
        }
    }
}
