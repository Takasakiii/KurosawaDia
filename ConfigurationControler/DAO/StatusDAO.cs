using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationControler.DAO
{
    public class StatusDAO
    {

        public async Task AdicionarAtualizarStatusAsync(Status[] statuses)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                await RemoverTabelaAsync(conexao);
                string sql = "insert into Status (status_jogo, status_tipo, status_url) values (@status, @tipo, @url)";
                for (int i = 0; i < statuses.Length; i++)
                {
                    SqliteCommand cmd = new SqliteCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@status", statuses[i].status_jogo);
                    cmd.Parameters.AddWithValue("@tipo", statuses[i].status_tipo);
                    cmd.Parameters.AddWithValue("@url", statuses[i].status_url);

                    await cmd.ExecuteNonQueryAsync();
                }
            });
        }

        private async Task RemoverTabelaAsync(SqliteConnection conexao)
        {
            string sql = "drop TABLE if EXISTS Status;";
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            await cmd.ExecuteNonQueryAsync();
            cmd = new SqliteCommand(DB.sqlCriacao[3], conexao);
            await cmd.ExecuteNonQueryAsync();
        }


        public async Task<Status[]> CarregarStatus()
        {
            List<Status> retorno = new List<Status>();
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "select * from Status;";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                SqliteDataReader rs = await cmd.ExecuteReaderAsync();

                while (await rs.ReadAsync())
                {
                    Status temp = new Status(Convert.ToUInt32(rs["status_id"]), (string)rs["status_jogo"], (Status.TiposDeStatus)Convert.ToInt32(rs["status_tipo"]), (string)rs["status_url"]);
                    retorno.Add(temp);
                }
            });

            return retorno.ToArray();
        }
    }
}
