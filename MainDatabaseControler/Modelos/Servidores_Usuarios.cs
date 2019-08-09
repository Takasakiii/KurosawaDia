namespace MainDatabaseControler.Modelos
{
    public class Servidores_Usuarios
    {
        public Servidores Servidor { get; private set; }
        public Usuarios Usuario { get; private set; }

        public Servidores_Usuarios(Servidores Servidor, Usuarios Usuario)
        {
            this.Servidor = Servidor;
            this.Usuario = Usuario;
        }
    }
}
