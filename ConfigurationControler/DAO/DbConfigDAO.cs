using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;
using System.Threading.Tasks;

namespace ConfigurationControler.DAO
{
    public class DbConfigDAO
    {
        public async Task<DBConfig> GetDbConfigAsync()
        {
            DBConfig dbConfig = null;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                SqliteCommand cmd = conexao.CreateCommand();
                cmd.CommandText = "select * from DbConfig";

                using (SqliteDataReader rs = await cmd.ExecuteReaderAsync())
                {
                    while (await rs.ReadAsync())
                    {
                        if (rs["porta"] != DBNull.Value)
                        {
                            dbConfig = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], Convert.ToInt32(rs["porta"]));
                        }
                        else
                        {
                            dbConfig = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], null);
                        }
                    }
                    rs.Close();
                }
            });
            return dbConfig;
        }
    }
}
