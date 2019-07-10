using Bot.DataBase.ConfigDB.Modelos;
using Bot.DataBase.Constructor;
using Microsoft.Data.Sqlite;

namespace Bot.DataBase.ConfigDB.DAO
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
                    ayuraConfig.SetBotConfig(reader.GetString(reader.GetOrdinal("token")), reader.GetString(reader.GetOrdinal("prefix")).ToCharArray());
                }
                reader.Close();
                conexao.Close();
                return ayuraConfig;
            }
        }
    }
}
