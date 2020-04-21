using DataBaseController.Abstractions;

namespace DataBaseController.Modelos
{
    public class AdmsBot
    {
        public ulong Cod { get; set; }
        public ulong CodUsuario { get; set; }
        public Usuarios Usuario { get; set; }
        public TiposAdms Permissao { get; set; }
    }
}
