using ConfigurationControler.ConfigDB.Modelos;
using ConfigurationControler.Factory;
using Microsoft.Data.Sqlite;

namespace ConfigurationControler.ConfigDB.DAO
{
    public class AyuraConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public AyuraConfig Carregar(AyuraConfig ayuraConfig)
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from DiaConfig where id = @id";
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
