namespace ConfigurationControler.Modelos
{
    public class DBConfig
    {
        public const uint id = 1;
        public string ip { private set; get; }
        public string database { private set; get; }
        public string login { private set; get; }
        public string senha { private set; get; }

        public DBConfig(string ip, string database, string login, string senha)
        {
            this.ip = ip;
            this.database = database;
            this.login = login;
            this.senha = senha;
        }
    }
}
