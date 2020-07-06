using DataBaseController.Abstractions;

namespace DataBaseController.Modelos
{
    public class ConfiguracoesServidores
    {
        public ulong Cod { get; set; }
        public TiposConfiguracoes Configuracoes { get; set; }
        public Servidores Servidor { get; set; }
        public ulong CodServidor { get; set; }
        public string Value { get; set; }
    }
}
