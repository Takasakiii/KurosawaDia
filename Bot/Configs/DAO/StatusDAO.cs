using Bot.Configs.Modelos;
using Bot.Constructor;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Bot.Configs.DAO
{
    public class StatusDAO
    {
        private SqliteConnection conexao = new SQLiteConstrutor().Conectar();

        public List<StatusConfig> getStatus()
        {
            List<StatusConfig> statusTmp = new List<StatusConfig>();

            SqliteCommand cmd = conexao.CreateCommand();
            cmd.CommandText = "select * from Status";

            using (SqliteDataReader reader = cmd.ExecuteReader())
            {

                while (reader.Read())
                {
                    StatusConfig tmp = new StatusConfig();
                    tmp.SetStatus(reader.GetInt32(reader.GetOrdinal("status_id")), reader.GetString(reader.GetOrdinal("status_jogo")), reader.GetInt32(reader.GetOrdinal("status_tipo")));
                    statusTmp.Add(tmp);
                }
                reader.Close();
                conexao.Close();
                return statusTmp;
            }
        }
    }
}
