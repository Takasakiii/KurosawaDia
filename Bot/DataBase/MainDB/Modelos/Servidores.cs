namespace Bot.DataBase.MainDB.Modelos
{
    public class Servidores
    {
        public long id { get; private set; }
        public string nome { get; private set; }
        public bool especial { get; private set; }
        public char[] prefix { get; private set; }
        public int codigo { get; private set; }

        public void SetServidor(long id, string nome, char[] prefix = null, bool especial = false, int codigo = 0)
        {
            this.id = id;
            this.nome = nome;
            this.especial = especial;
            this.prefix = prefix;
            this.codigo = codigo;
        }

        public void SetPrefix(char[] prefix)
        {
            this.prefix = prefix;
        }

        public void SetId(long id)
        {
            this.id = id;
        }
    }
}
