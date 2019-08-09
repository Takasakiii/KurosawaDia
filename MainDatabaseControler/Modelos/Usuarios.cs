namespace MainDatabaseControler.Modelos
{
    public class Usuarios
    {
        public ulong Id { get; private set; }
        public string Nome { get; private set; }
        public ulong Cod { get; private set; }

        public Usuarios(ulong Id, string Nome, ulong Cod = 0)
        {
            this.Id = Id;
            this.Nome = Nome;
            this.Cod = Cod;
        }
    }
}
