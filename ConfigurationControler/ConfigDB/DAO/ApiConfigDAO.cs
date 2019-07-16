using ConfigurationControler.ConfigDB.Modelos;
using ConfigurationControler.Factory;
using Microsoft.Data.Sqlite;

namespace ConfigurationControler.ConfigDB.DAO
{
    public class ApiConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public ApiConfig Carregar(ApiConfig apiConfig)
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from ApiConfig where id = @id";
            selectCmd.Parameters.AddWithValue("@id", apiConfig.id);

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    apiConfig.SetApiConfig(reader.GetString(reader.GetOrdinal("WeebToken")));
                }
                reader.Close();
                conexao.Close();
                return apiConfig;
            }
        }
    }
}
