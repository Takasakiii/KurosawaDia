namespace MainDatabaseControler.Modelos
{
    public class Insultos
    {
        public string Insulto { get; private set; }
        public Usuarios Usuario { get; private set; }
        public ulong Cod { get; private set; }

        public Insultos(string Insulto, Usuarios Usuario, ulong Cod = 0)
        {
            this.Insulto = Insulto;
            this.Usuario = Usuario;
            this.Cod = Cod;
        }

        public Insultos(ulong Cod = 0)
        {
            this.Cod = Cod;
        }
    }
}
