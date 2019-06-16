using Bot.Singletons;
using Microsoft.Data.Sqlite;

namespace Bot.DataBase.Constructor
{
    public class SQLiteConstrutor
    {
        public SqliteConnection Conectar()
        {
            SqliteConnection conexao = new SqliteConnection($"Data Source={SingletonConfig.localConfig}");
            conexao.Open();
            return conexao;
        }
    }
}
