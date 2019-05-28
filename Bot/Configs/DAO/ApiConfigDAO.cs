using Bot.Constructor;
using Bot.Modelos;
using Microsoft.Data.Sqlite;

namespace Bot.DAO
{
    public class ApiConfigDAO
    {
        private SqliteConnection conexao = new SQLiteConstrutor().Conectar();

        public ApiConfig Carregar(ApiConfig apiConfig)
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from ApiConfig where id = @id";
            selectCmd.Parameters.AddWithValue("@id", apiConfig.id);

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    apiConfig.setApiConfig(reader.GetString(reader.GetOrdinal("WeebToken")));
                }
                conexao.Close();
                return apiConfig;
            }
        }
    }
}
