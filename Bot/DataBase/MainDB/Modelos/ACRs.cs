namespace Bot.DataBase.MainDB.Modelos
{
    public class ACRs
    {
        public uint codigo { get; private set; }
        public string trigger { get; private set; }
        public string resposta { get; private set; }
        public ulong id_servidor { get; private set; }

        public void SetAcr(uint codigo, string trigger, string resposta, ulong id_servidor)
        {
            this.codigo = codigo;
            this.trigger = trigger;
            this.resposta = resposta;
            this.id_servidor = id_servidor;
        }

        public void SetTrigger(string trigger, ulong id_servidor)
        {
            this.trigger = trigger;
            this.id_servidor = id_servidor;
        }

        public void SetResposta(string resposta)
        {
            this.resposta = resposta;
        }
    }
}
