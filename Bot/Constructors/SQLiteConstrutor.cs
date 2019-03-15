using Bot.Singletons;
using System.Data.SQLite;

namespace Bot.Constructors
{
    public class SQLiteConstrutor
    {
        public SQLiteConnection Conectar()
        {
            SQLiteConnection conexao = new SQLiteConnection($"Data Source={SingletonConfig.localConfig}");
            conexao.Open();
            return conexao;
        }
    }
}
