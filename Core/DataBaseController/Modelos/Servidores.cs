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
        public List<ConfiguracoesServidoresAplicada> Configuracoes { get; set; }
    }
}
