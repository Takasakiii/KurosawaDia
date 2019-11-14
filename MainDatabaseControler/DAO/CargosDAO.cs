using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace MainDatabaseControler.DAO
{
    public class CargosDAO
    {
        public enum Operacao
        {
            Incompleta = 0,
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        public async Task<Operacao> AdicionarAtualizarCargoAsync(Cargos cargos)
        {
            Operacao retorno = Operacao.Incompleta;

            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call AdicionarAtualizarCargoIP(@cargo, @idC, @idS, @IP)";

                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@cargo", cargos.Cargo);
                cmd.Parameters.AddWithValue("@idC", cargos.Id);
                cmd.Parameters.AddWithValue("@idS", cargos.Servidor.Id);
                cmd.Parameters.AddWithValue("@IP", cargos.Requesito);

                DbDataReader rs = await cmd.ExecuteReaderAsync();

                if (await rs.ReadAsync())
                {
                    if (rs["tipoOperacao"] != null)
                    {
                        retorno = (Operacao)Convert.ToInt32(rs["tipoOperacao"]);
                    }
                }

            });

            return retorno;
        }
    }
}
