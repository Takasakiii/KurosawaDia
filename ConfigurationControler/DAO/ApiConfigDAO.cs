using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;

namespace ConfigurationControler.DAO
{
    public class ApiConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public ApiConfig Carregar()
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from ApiConfig where id = @id";
            selectCmd.Parameters.AddWithValue("@id", ApiConfig.id);

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                ApiConfig apiConfig = null;
                if (reader.Read())
                {
                    apiConfig = new ApiConfig((string)reader["WeebToken"], (string)reader["dblToken"]);
                }
                reader.Close();
                conexao.Close();
                return apiConfig;
            }
        }
    }
}
