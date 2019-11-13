using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace ConfigurationControler.DAO
{
    public class DiaConfigDAO
    {

        public async Task<DiaConfig> CarregarAsync()
        {
            DiaConfig diaConfig = null;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                SqliteCommand selectCmd = conexao.CreateCommand();
                selectCmd.CommandText = "select * from DiaConfig where id = @id";
                selectCmd.Parameters.AddWithValue("@id", DiaConfig.id);

                using (SqliteDataReader reader = await selectCmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        diaConfig = new DiaConfig(reader.GetString(reader.GetOrdinal("token")), reader.GetString(reader.GetOrdinal("prefix")), Convert.ToUInt64(reader["idDono"]));
                    }

                }

            });

            return diaConfig;
        }
    }
}
