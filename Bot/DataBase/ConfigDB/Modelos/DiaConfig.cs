namespace Bot.DataBase.ConfigDB.Modelos
{
    public class DiaConfig
    {
        public string token { private set; get; }
        public char[] prefix { private set; get; }
        public uint id { private set; get; }

        public DiaConfig(uint id, string token = null, char[] prefix = null)
        {
            this.token = token;
            this.prefix = prefix;
            this.id = id;
        }

        public void SetBotConfig(string token, char[] prefix)
        {
            this.token = token;
            this.prefix = prefix;
        }
    }
}