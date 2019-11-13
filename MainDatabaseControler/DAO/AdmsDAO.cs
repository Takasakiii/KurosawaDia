using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Adms;

namespace MainDatabaseControler.DAO
{
    public class AdmsDAO
    {

        public async Task<Tuple<bool, Adms>> GetAdmAsync(Adms adms)
        {
            bool retorno = false;
            Adms retadms = adms;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetAdm(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", adms.Usuario.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                
                if (await rs.ReadAsync())
                {
                    bool result = Convert.ToBoolean(rs["result"]);
                    if (result)
                    {
                        retadms.SetPerms((PermissoesAdms)rs["permissao"]);
                        retorno = true;
                    }
                }
            });

            return Tuple.Create(retorno, retadms);
        }

        public void SetAdm(Adms adms)
        {
            const string sql = "call AdicionarAdm(@id, @perm)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", adms.Usuario.Id);
            cmd.Parameters.AddWithValue("@perm", (int)adms.Permissoes);

            cmd.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
