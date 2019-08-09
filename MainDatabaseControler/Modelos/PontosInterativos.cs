namespace MainDatabaseControler.Modelos
{
    public class PontosInterativos
    {
        public Servidores_Usuarios Servidores_Usuarios { get; private set; }
        public ulong PI { get; private set; }
        public ulong FragmentosPI { get; private set; }
        public ulong Cod { get; private set; }

        public PontosInterativos(Servidores_Usuarios Servidores_Usuarios, ulong Cod = 0)
        {
            this.Servidores_Usuarios = Servidores_Usuarios;
            this.Cod = Cod;
        }

        public void AddPIInfo(ulong Cod, ulong PI, ulong FragmentosPI)
        {
            this.Cod = Cod;
            this.PI = PI;
            this.FragmentosPI = FragmentosPI;
        }
    }
}
