using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace MainDatabaseControler.DAO
{
    public class FuckDAO
    {
        public async Task<Tuple<bool, Fuck>> GetImgAsync(Fuck fuck)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetFuckImg(@explicit)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@explicit", fuck.ExplicitImg);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    Usuarios usuario = new Usuarios(Convert.ToUInt64(rs["id_usuario"]), (string)rs["nome_usuario"]);
                    fuck = new Fuck(Convert.ToBoolean(rs["explicitImage"]), (string)rs["urlImage"], usuario, Convert.ToUInt32(rs["cod"]));
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, fuck);
        }

        public async Task AddImgAsync(Fuck fuck)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call AdicionarImgFuck(@id, @img, @explicit)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", fuck.Usuario.Id);
                cmd.Parameters.AddWithValue("@img", fuck.Img);
                cmd.Parameters.AddWithValue("@explicit", fuck.ExplicitImg);

                await cmd.ExecuteNonQueryAsync();
            });
        }

    }
}
