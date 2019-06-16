using Bot.DataBase.ConfigDB.Modelos;
using Bot.DataBase.Constructor;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;

namespace Bot.DataBase.ConfigDB.DAO
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
