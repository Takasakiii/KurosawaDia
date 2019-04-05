using Bot.Configs.Modelos;
using Bot.Constructor;
using System.Data.SQLite;

namespace Bot.Configs.DAO
{
    public class DbConfigDAO
    {
        private SQLiteConnection conexao = new SQLiteConstrutor().Conectar();

        public DBconfig Carregar(DBconfig DataBaseaConf)
        {
            using (SQLiteCommand cmd = new SQLiteCommand(conexao))
            {
                cmd.CommandText = "select * from DbConfig where id = @id";
                cmd.Parameters.AddWithValue("@id", DataBaseaConf.id);
                SQLiteDataReader rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    DataBaseaConf.SetDbConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"]);
                }
                conexao.Close();
                return DataBaseaConf;
            }
        }
    }
}
