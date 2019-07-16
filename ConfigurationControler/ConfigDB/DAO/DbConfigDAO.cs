using ConfigurationControler.ConfigDB.Modelos;
using ConfigurationControler.Factory;
using Microsoft.Data.Sqlite;

namespace ConfigurationControler.ConfigDB.DAO
{
    public class DbConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public DbConfig GetDbConfig()
        {
            SqliteCommand cmd = conexao.CreateCommand();
            cmd.CommandText = "select * from DbConfig";

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                DbConfig dbConfig = new DbConfig();
                while (reader.Read())
                {
                    dbConfig.SetDb(reader.GetInt32(reader.GetOrdinal("id")), reader.GetString(reader.GetOrdinal("ip")), reader.GetString(reader.GetOrdinal("database")), reader.GetString(reader.GetOrdinal("login")), reader.GetString(reader.GetOrdinal("senha")));
                }
                reader.Close();
                conexao.Close();
                return dbConfig;
            }
        }
    }
}
