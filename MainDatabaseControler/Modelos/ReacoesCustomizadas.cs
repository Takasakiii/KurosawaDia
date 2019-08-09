namespace MainDatabaseControler.Modelos
{
    public class ReacoesCustomizadas
    {
        public string Trigger { get; private set; }
        public string Resposta { get; private set; }
        public Servidores Servidor { get; private set; }
        public ulong Cod { get; private set; }

        public ReacoesCustomizadas SetTrigger(string Trigger, Servidores Servidor = null)
        {
            this.Trigger = Trigger;
            this.Servidor = Servidor;

            return this;
        }

        public ReacoesCustomizadas SetResposta(string Trigger, Servidores Servidor = null)
        {
            this.Trigger = Trigger;
            this.Servidor = Servidor;

            return this;
        }

        public ReacoesCustomizadas SetServidor(Servidores Servidor = null)
        {
            this.Servidor = Servidor;

            return this;
        }

        public ReacoesCustomizadas(ulong Cod = 0)
        {
            this.Cod = Cod;
        }

        public ReacoesCustomizadas(string Trigger, string Resposta, Servidores Servidor, ulong Cod)
        {
            this.Trigger = Trigger;
            this.Resposta = Resposta;
            this.Servidor = Servidor;
            this.Cod = Cod;
        }
    }
}
