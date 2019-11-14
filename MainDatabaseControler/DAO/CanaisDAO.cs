using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Canais;

namespace MainDatabaseControler.DAO
{
    public class CanaisDAO
    {
        public async Task<bool> AddChAsync(Canais canal)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call setCh(@tipo, @nome, @id, @idServidor)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@tipo", (int)canal.TipoCanal);
                cmd.Parameters.AddWithValue("@nome", canal.NomeCanal);
                cmd.Parameters.AddWithValue("@id", canal.Id);
                cmd.Parameters.AddWithValue("@idServidor", canal.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    retorno = Convert.ToBoolean(rs["result"]);
                }
            });
            return retorno;
        }

        public async Task<Tuple<bool, Canais>> GetChAsync(Canais canal)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetCh(@tipo, @id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@tipo", (int)canal.TipoCanal);
                cmd.Parameters.AddWithValue("@id", canal.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    canal = new Canais(Convert.ToUInt64(rs["id"]), new Servidores(Convert.ToUInt64(rs["id_servidor"]), (string)rs["nome_servidor"]), canal.TipoCanal, (string)rs["nome"]);
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, canal);
        }
    }
}
