namespace DataBaseController.Modelos
{
    public class Servidores_Usuarios
    {
        public ulong ServidorCod { get; set; }
        public Servidores Servidor { get; set; }
        public ulong UsuarioCod { get; set; }
        public Usuarios Usuario { get; set; }
    }
}
