namespace MainDatabaseControler.Modelos
{
    public class Canais
    {
        public enum TiposCanais { bemvindoCh, sairCh }

        public ulong Id { get; private set; }
        public Servidores Servidor { get; private set; }
        public TiposCanais TipoCanal { get; private set; }
        public string NomeCanal { get; private set; }
        public ulong Cod { get; private set; }

        public Canais(ulong Id, ulong Cod = 0)
        {
            this.Id = Id;
            this.Cod = Cod;
        }

        public Canais(ulong Id, Servidores Servidor, ulong Cod = 0)
        {
            this.Id = Id;
            this.Servidor = Servidor;
            this.Cod = Cod;
        }

        public Canais(ulong Id, TiposCanais TipoCanal, ulong Cod = 0)
        {
            this.Id = Id;
            this.TipoCanal = TipoCanal;
            this.Cod = Cod;
        }


        public Canais(ulong Id, string NomeCanal, ulong Cod = 0)
        {
            this.Id = Id;
            this.NomeCanal = NomeCanal;
            this.Cod = Cod;
        }

        public Canais(Servidores Servidor, TiposCanais TipoCanal, ulong Cod = 0)
        {
            this.Servidor = Servidor;
            this.TipoCanal = TipoCanal;
            this.Cod = Cod;
        }

        public Canais(ulong Id, Servidores Servidor, TiposCanais TipoCanal, string NomeCanal, ulong Cod = 0)
        {
            this.Id = Id;
            this.Servidor = Servidor;
            this.TipoCanal = TipoCanal;
            this.NomeCanal = NomeCanal;
            this.Cod = Cod;
        }
    }
}
