using Bot.DataBase.ConfigDB.Modelos;
using Bot.DataBase.Constructor;
using Microsoft.Data.Sqlite;

namespace Bot.DataBase.ConfigDB.DAO
{
    public class DbConfigDAO
    {
        private SqliteConnection conexao = new SQLiteConstrutor().Conectar();

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
