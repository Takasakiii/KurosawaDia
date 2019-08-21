namespace MainDatabaseControler.Modelos
{
    public class Fuck
    {
        public string Img { get; private set; }
        public bool ExplicitImg { get; private set; }
        public Usuarios Usuario { get; private set; }
        public ulong Cod { get; private set; }

        public Fuck(bool ExplicitImg, ulong Cod = 0)
        {
            this.ExplicitImg = ExplicitImg;
            this.Cod = Cod;
        }

        public Fuck(bool ExplicitImg, string Img, ulong Cod = 0)
        {
            this.ExplicitImg = ExplicitImg;
            this.Img = Img;
            this.Cod = Cod;
        }

        public Fuck(bool ExplicitImg, string Img, Usuarios Usuario, ulong Cod = 0)
        {
            this.ExplicitImg = ExplicitImg;
            this.Img = Img;
            this.Usuario = Usuario;
            this.Cod = Cod;
        }
    }
}
