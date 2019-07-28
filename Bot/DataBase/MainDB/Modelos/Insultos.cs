namespace Bot.DataBase.MainDB.Modelos
{
    public class Insultos
    {
        public uint codigo { get; private set; }
        public Usuarios usuario { get; private set; }
        public string insulto { get; private set; }

        public void SetInsulto(string insulto, Usuarios usuario, uint codigo = 0)
        {
            this.codigo = codigo;
            this.usuario = usuario;
            this.insulto = insulto;
        }
    }
}
