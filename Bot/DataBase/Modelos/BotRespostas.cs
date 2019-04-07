namespace Bot.Modelos.Objetos
{
    public class BotRespostas
    {

        //restrições invalidas
        public long id { get; set; }
        public string pergunta { get; set; }
        public string resposta { get; set; }
        public Servidores servidor { get; set; }

        public BotRespostas()
        {
            servidor = new Servidores(); //Construtor invalido
        }
    }
}
