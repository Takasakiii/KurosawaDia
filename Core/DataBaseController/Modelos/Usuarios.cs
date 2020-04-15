using System.Collections.Generic;

namespace DataBaseController.Modelos
{
    public class Usuarios
    {
        public ulong Cod { get; set; }
        public ulong ID { get; set; }
        public string Nome { get; set; }
        public virtual List<Servidores_Usuarios> ServidoresUsuarios { get; set; }
        public virtual List<Insultos> Insultos { get; set; }
        public virtual List<AdmsBot> AdmsBots { get; set; }
        public virtual List<Fuck> Fuck { get; set; }
    }
}
