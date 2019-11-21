using Discord;

namespace Bot.Modelos
{
    public class ImgModel
    {
        public IMessageChannel Canal { get; private set; }
        public string NomeUsuario { get; private set; }
        public string Texto { get; private set; }
        public bool Nsfw { get; private set; }
        public int Quantidade { get; private set; }

        public ImgModel(IMessageChannel Canal, string NomeUsuario = "", string Texto = "", bool Nsfw = false, int Quantidade = 1)
        {
            this.Canal = Canal;
            this.NomeUsuario = NomeUsuario;
            this.Texto = Texto;
            this.Nsfw = Nsfw;
            this.Quantidade = Quantidade;
        }
    }
}
