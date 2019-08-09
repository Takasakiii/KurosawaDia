namespace MainDatabaseControler.Modelos
{
    public class Servidores
    {
        public enum PermissoesServidores { Normal, ServidorPika, LolisEdition }

        public ulong Id { get; private set; }
        public PermissoesServidores Permissoes { get; private set; }
        public char[] Prefix { get; private set; }
        public string Nome { get; private set; }
        public ulong Cod { get; private set; }

        public Servidores(ulong Id, ulong Cod = 0)
        {
            this.Id = Id;
            this.Cod = Cod;
        }

        public Servidores(ulong Id, PermissoesServidores Permissoes, ulong Cod = 0)
        {
            this.Id = Id;
            this.Permissoes = Permissoes;
            this.Cod = Cod;
        }

        public Servidores(ulong Id, char[] Prefix, ulong Cod = 0)
        {
            this.Id = Id;
            this.Prefix = Prefix;
            this.Cod = Cod;
        }

        public Servidores(ulong Id, string Nome, ulong Cod = 0)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Cod = Cod;
        }

        public Servidores(ulong Id, PermissoesServidores Permissoes, char[] Prefix, string Nome, ulong Cod = 0)
        {
            this.Id = Id;
            this.Permissoes = Permissoes;
            this.Prefix = Prefix;
            this.Nome = Nome;
            this.Cod = Cod;
        }
    }
}
