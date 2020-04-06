using System.Collections.Generic;

namespace DataBaseController.Modelos
{
    public class Servidores
    {
        public ulong Cod { get; set; }
        public ulong ID { get; set; }
        public string Nome { get; set; }
        public bool Espercial { get; set; }
        public string Prefix { get; set; }
        public virtual List<ConfiguracoesServidores> Configuracoes { get; set; }
        public virtual List<Servidores_Usuarios> ServidoresUsuarios { get; set; }
        public virtual List<CustomReactions> CustomReactions { get; set; }
        public virtual List<Cargos> Cargos { get; set; }
        public virtual List<Canais> Canais { get; set; }
    }
}
