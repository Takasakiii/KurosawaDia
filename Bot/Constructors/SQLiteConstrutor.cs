using Bot.Singletons;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

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
