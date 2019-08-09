namespace MainDatabaseControler.Modelos
{
    public class Adms
    {
        public enum PermissoesAdms { Donas };

        public PermissoesAdms Permissoes { get; private set; }
        public Usuarios Usuario { get; private set; }
        public ulong Cod { get; private set; }

        public Adms(Usuarios Usuario)
        {
            this.Usuario = Usuario;
        }

        public Adms SetPerms(PermissoesAdms Permissoes)
        {
            this.Permissoes = Permissoes;

            return this;
        }
    }
}
