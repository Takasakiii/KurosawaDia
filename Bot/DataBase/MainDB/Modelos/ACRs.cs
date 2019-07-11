namespace Bot.DataBase.MainDB.Modelos
{
    public class ACRs
    {
        public ulong codigo { get; private set; }
        public string trigger { get; private set; }
        public string resposta { get; private set; }
        public Servidores servidores { get; private set; }

        public void SetAcr(string trigger, string resposta, Servidores servidores, ulong codigo = 0)
        {
            this.codigo = codigo;
            this.trigger = trigger;
            this.resposta = resposta;
            this.servidores = servidores;
        }

        public void SetTrigger(string trigger, Servidores servidores)
        {
            this.trigger = trigger;
            this.servidores = servidores;
        }

        public void SetResposta(string resposta)
        {
            this.resposta = resposta;
        }

        public void SetServidor (Servidores servidores)
        {
            this.servidores = servidores;
        }

        public void SetCod(ulong codigo, Servidores servidores)
        {
            this.codigo = codigo;
            this.servidores = servidores;
        }
    }
}
