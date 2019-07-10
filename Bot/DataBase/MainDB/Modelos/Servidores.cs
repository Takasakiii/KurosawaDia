namespace Bot.DataBase.MainDB.Modelos
{
    public class Servidores
    {
        public ulong id { get; private set; }
        public string nome { get; private set; }
        public bool especial { get; private set; }
        public char[] prefix { get; private set; }
        public uint codigo { get; private set; }

        private void SetServidor(ulong id = 0, string nome = null, char[] prefix = null, bool especial = false, uint codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.especial = especial;
            this.prefix = prefix;
            this.codigo = codigo;
        }

        public void SetId(ulong id)
        {
            SetServidor(id);
        }

        public void SetNome(ulong id, string nome)
        {
            SetServidor(id, nome);
        }

        public void SetPrefix(ulong id, char[] prefix)
        {
            SetServidor(id, prefix: prefix);
        }
    }
}
