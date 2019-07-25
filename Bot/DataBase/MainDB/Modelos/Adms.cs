namespace Bot.DataBase.MainDB.Modelos
{
    public class Adms
    {
        public enum PermissoesAdm { Donas };

        public uint codigo { get; private set; }
        public Usuarios usuario { get; private set; }
        public PermissoesAdm permissoes { get; private set; }

        public Adms(Usuarios usuario)
        {
            this.usuario = usuario;
        }

        public void SetPerms(PermissoesAdm permissoes)
        {
            this.permissoes = permissoes;
        }
    }
}
