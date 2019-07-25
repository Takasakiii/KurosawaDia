namespace Bot.DataBase.MainDB.Modelos
{
    public class Servidores
    {
        public enum Permissoes { Normal, ServidorBot, LolisEdition}
        public ulong codigo { get; private set; }
        public ulong id { get; private set; }
        public string nome { get; private set; }
        public Permissoes permissoes { get; private set; }
        public char[] prefix { get; private set; }
        

        private void SetServidor(ulong id = 0, string nome = null, char[] prefix = null, Permissoes permissoes = Permissoes.Normal, ulong codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.permissoes = permissoes;
            this.prefix = prefix;
            this.codigo = codigo;
        }

        public Servidores(ulong id, ulong codigo = 0)
        {
            SetServidor(id, codigo: codigo);
        }

        public Servidores(ulong id, string nome)
        {
            SetServidor(id, nome);
        }

        public void SetPrefix(char[] prefix)
        {
            SetServidor(id, prefix: prefix);
        }

        public void SetPermissao(Permissoes permissoes)
        {
            this.permissoes = permissoes;
        }
    }
}
