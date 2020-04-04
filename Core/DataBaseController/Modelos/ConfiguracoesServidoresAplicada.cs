namespace DataBaseController.Modelos
{
    public class ConfiguracoesServidoresAplicada
    {
        public ulong Cod { get; set; }
        
        public Servidores Servidor { get; set; }
        public ConfiguracoesServidores Configuracao { get; set; }
        public string Value { get; set; }
    }
}
