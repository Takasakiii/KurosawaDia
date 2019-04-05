using Bot.Constructor;
using Bot.Modelos;
using System.Data.SQLite;

namespace Bot.DAO
{
    public class ApiConfigDAO
    {
        private SQLiteConnection conexao = new SQLiteConstrutor().Conectar();

        public ApiConfig Carregar(ApiConfig apiConfig)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(conexao))
            {
                cmd.CommandText = "select * from ApiConfig where id = @id";
                cmd.Parameters.AddWithValue("@id", apiConfig.id);
                SQLiteDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    apiConfig.setApiConfig((string)rs["WeebToken"]);
                }
                conexao.Close();
                return apiConfig;
            }
        }
    }
}
