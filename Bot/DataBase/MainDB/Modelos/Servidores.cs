namespace Bot.DataBase.MainDB.Modelos
{
    public class Servidores
    {
        public ulong codigo { get; private set; }
        public ulong id { get; private set; }
        public string nome { get; private set; }
        public bool especial { get; private set; }
        public char[] prefix { get; private set; }
        

        private void SetServidor(ulong id = 0, string nome = null, char[] prefix = null, bool especial = false, ulong codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.especial = especial;
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
    }
}
