namespace Bot.DataBase.MainDB.Modelos
{
    public class Canais
    {
        public enum TiposCanais { bemvindoCh, sairCh }

        public TiposCanais tipoCanal { get; private set; }
        public ulong id { get; private set; }
        public Servidores servidor { get; private set; }
        public string canal { get; private set; }
        public ulong cod { get; private set; }

        public Canais(TiposCanais tipoCanal, ulong id, Servidores servidor, string canal, ulong cod = 0)
        {
            this.tipoCanal = tipoCanal;
            this.id = id;
            this.servidor = servidor;
            this.canal = canal;
            this.cod = cod;
        }
    }
}
