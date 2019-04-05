namespace Bot.Configs.Modelos
{
    public class DBconfig
    {
        public uint id { private set; get; }
        public string ip { private set; get; }
        public string db { private set; get; }
        public string login { private set; get; }
        public string senha { private set; get; }

        public DBconfig(uint id, string ip = null, string db = null, string login = null, string senha = null)
        {
            this.id = id;
            this.ip = ip;
            this.db = db;
            this.login = login;
            this.senha = senha;
        }

        public void SetDbConfig(string ip, string db, string login, string senha)
        {
            this.ip = ip;
            this.db = db;
            this.login = login;
            this.senha = senha;
        }
    }
}
