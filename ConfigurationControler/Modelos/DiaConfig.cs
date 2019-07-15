namespace ConfigurationControler.Modelos
{
    public class DiaConfig
    {
        public const uint id = 1;
        public string token { private set; get; }
        public string prefix { private set; get; }
        public ulong idDono { private set; get; }

        public DiaConfig(string token, string prefix, ulong idDono)
        {
            this.token = token;
            this.prefix = prefix;
            this.idDono = idDono;
        }
    }
}
