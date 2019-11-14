using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.Servidores;

namespace MainDatabaseControler.DAO
{
    public class ServidoresDAO
    {
        public async Task<Tuple<bool, Servidores>> GetPrefixAsync(Servidores servidor)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call buscarPrefix(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                string prefix = null;
                if (await rs.ReadAsync())
                {
                    prefix = rs["prefix_servidor"].ToString();
                }

                if (!string.IsNullOrEmpty(prefix))
                {
                    servidor = new Servidores(servidor.Id, prefix.ToCharArray());
                    retorno = true;
                }
            });

            return Tuple.Create(retorno, servidor);
        }

        public async Task<Tuple<bool, Servidores>> SetServidorPrefixAsync(Servidores servidor)
        {
            bool retono = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call atualizarPrefix(@id, @prefix)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", servidor.Id);
                cmd.Parameters.AddWithValue("@prefix", new string(servidor.Prefix));

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    servidor = new Servidores(servidor.Id, ((string)rs["prefix_servidor"]).ToCharArray());
                    retono = true;
                }
            });
            return Tuple.Create(retono, servidor);
        }

        public async Task<Tuple<bool, Servidores>> GetPermissoesAsync(Servidores servidor)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetPermissoes(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    servidor = new Servidores(servidor.Id, (PermissoesServidores)rs["especial_servidor"], servidor.Prefix, servidor.Nome);
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, servidor);
        }

        public async Task<bool> SetEspecialAsync(Servidores servidor)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call DefinirTipoServidor(@id, @tipo)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", servidor.Id);
                cmd.Parameters.AddWithValue("@tipo", (int)servidor.Permissoes);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    retorno = Convert.ToBoolean(rs["Result"]);
                }
            });
            return retorno;
        }
    }
}
