using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using static MainDatabaseControler.Modelos.Adms;

namespace Bot.Extensions
{
    public class AdmsExtensions
    {
        public Tuple<bool, PermissoesAdms> GetAdm(Usuarios usuario)
        {
            DiaConfig diaConfig = new DiaConfigDAO().Carregar();
            if(usuario.Id != diaConfig.idDono)
            {
                Adms adms = new Adms(usuario);
                if(new AdmsDAO().GetAdm(ref adms))
                {
                    return Tuple.Create(true, adms.Permissoes);
                }
                else
                {
                    return Tuple.Create(false, PermissoesAdms.Nada);
                }
            }
            else
            {
                return Tuple.Create(true, PermissoesAdms.Donas);
            }
        }
    }
}
