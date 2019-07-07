using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.ConfigDB.Modelos
{
    public class DbConfig
    {
        public int id { get; private set; }
        public string ip { get; private set; }
        public string database { get; private set; }
        public string login { get; private set; }
        public string senha { get; private set; }

        public void SetDb(int id, string ip, string database, string login, string senha)
        {
            this.id = id;
            this.ip = ip;
            this.database = database;
            this.login = login;
            this.senha = senha;
        }
    }
}
