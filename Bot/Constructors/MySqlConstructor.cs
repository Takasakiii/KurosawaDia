using Bot.Configs.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.Constructor
{
    public class MySqlConstructor
    {
        public MySqlConnection Conectar (DBconfig dBconfig)
        {
            MySqlConnection sql = new MySqlConnection($"Server={dBconfig.ip};Database={dBconfig.db};Uid={dBconfig.login};Pwd={dBconfig.senha}");
            sql.Open();
            return sql;
            //n considero
        }
    }
}
