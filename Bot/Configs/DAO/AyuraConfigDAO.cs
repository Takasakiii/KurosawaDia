using Bot.Constructor;
using Bot.Modelos;
using Microsoft.Data.Sqlite;

namespace Bot.DAO
{
    public class AyuraConfigDAO
    {
        private SqliteConnection conexao = new SQLiteConstrutor().Conectar();

        public AyuraConfig Carregar(AyuraConfig ayuraConfig)
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from AyuraConfig where id = @id";
            selectCmd.Parameters.AddWithValue("@id", ayuraConfig.id);

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    ayuraConfig.SetBotConfig(reader.GetString(1), reader.GetString(2).ToCharArray());
                }
                conexao.Close();
                return ayuraConfig;
            }
        }
    }
}
