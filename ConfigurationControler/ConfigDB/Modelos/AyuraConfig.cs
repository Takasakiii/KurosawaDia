namespace ConfigurationControler.ConfigDB.Modelos
{
    public class AyuraConfig
    {
        public string token { private set; get; }
        public char[] prefix { private set; get; }
        public uint id { private set; get; }

        public AyuraConfig(uint id, string token = null, char[] prefix = null)
        {
            this.id = id;
            this.token = token;
            this.prefix = prefix;
        }

        public void SetBotConfig(string token, char[] prefix)
        {
            this.token = token;
            this.prefix = prefix;
        }
    }
}
