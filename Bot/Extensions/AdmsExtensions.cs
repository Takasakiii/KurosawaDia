using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MainDatabaseControler.DAO;
using MainDatabaseControler.Modelos;
using System;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Adms;

namespace Bot.Extensions
{
    public class AdmsExtensions
    {
        public async Task<Tuple<bool, PermissoesAdms>> GetAdm(Usuarios usuario)
        {
            DiaConfig diaConfig = await new DiaConfigDAO().CarregarAsync();
            if(usuario.Id != diaConfig.idDono)
            {
                Adms adms = new Adms(usuario);
                Tuple<bool, Adms> temp = await new AdmsDAO().GetAdmAsync(adms);
                adms = temp.Item2;
                if (temp.Item1)
                {
                    if(adms.Permissoes != PermissoesAdms.Nada)
                    {
                        return Tuple.Create(true, adms.Permissoes);
                    }
                    else
                    {
                        return Tuple.Create(false, adms.Permissoes);
                    }
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
