using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace MainDatabaseControler.DAO
{
    public class InsultosDAO
    {
        public async Task<bool> InserirInsultoAsync(Insultos insulto)
        {
            bool retorno = true;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                try
                {
                    const string sql = "call AdicionarInsulto(@id, @insulto)";
                    MySqlCommand cmd = new MySqlCommand(sql, conexao);

                    cmd.Parameters.AddWithValue("@id", insulto.Usuario.Id);
                    cmd.Parameters.AddWithValue("@insulto", insulto.Insulto);

                    await cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    retorno = false;
                }
            });
            return retorno;
        }

        public async Task<Tuple<bool, Insultos>> GetInsultoAsync(Insultos insulto)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call PegarInsulto()";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    Usuarios usuario = new Usuarios(Convert.ToUInt64(rs["id_usuario"]), (string)rs["nome_usuario"]);
                    insulto = new Insultos((string)rs["insulto"], usuario, Convert.ToUInt32(rs["cod"]));
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, insulto);
        }
    }
}
