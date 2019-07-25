namespace ConfigurationControler.Modelos
{
    public class ApisConfig
    {
        public uint id { private set; get; }
        public string ApiIdentifier { private set; get; }
        public string Token { private set; get; }
        
        public bool Ativada { private set; get; }

        public ApisConfig(string ApiIdentifier, string Token, bool Ativada, uint id = 0)
        {
            this.ApiIdentifier = ApiIdentifier;
            this.Token = Token;
            this.Ativada = Ativada;
            this.id = id;
        }
    }
}
