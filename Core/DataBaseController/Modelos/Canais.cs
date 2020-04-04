using DataBaseController.Abstractions;

namespace DataBaseController.Modelos
{
    public class Canais
    {
        public ulong Cod { get; set; }
        public TiposCanais TipoCanal { get; set; }
        public string Nome { get; set; }
        public ulong ID { get; set; }
        public Servidores Servidor { get; set; }
    }
}
