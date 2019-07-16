using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;

namespace ConfigurationControler.DAO
{
    public class DbConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public DBConfig GetDbConfig()
        {
            SqliteCommand cmd = conexao.CreateCommand();
            cmd.CommandText = "select * from DbConfig";

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {
                DBConfig dbConfig = null;
                while (reader.Read())
                {
                    dbConfig = new DBConfig(reader.GetString(reader.GetOrdinal("ip")), reader.GetString(reader.GetOrdinal("database")), reader.GetString(reader.GetOrdinal("login")), reader.GetString(reader.GetOrdinal("senha")));
                }
                reader.Close();
                conexao.Close();
                return dbConfig;
            }
        }
    }
}
