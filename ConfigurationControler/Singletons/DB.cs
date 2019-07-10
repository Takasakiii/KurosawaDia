using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.Singletons
{
    public static class DB
    {

        public readonly static string[] sqlCriacao = { "CREATE TABLE \"ApiConfig\" (\"id\"INTEGER PRIMARY KEY,\"WeebToken\" TEXT NOT NULL)",
                "CREATE TABLE \"DiaConfig\" (\"id\" INTEGER PRIMARY KEY,\"token\" TEXT NOT NULL,\"prefix\" TEXT,\"idDono\" INTEGER NOT NULL)",
                "CREATE TABLE \"DbConfig\" (\"id\"    INTEGER PRIMARY KEY,\"ip\"    TEXT NOT NULL,\"database\"  TEXT NOT NULL,\"login\" TEXT NOT NULL,\"senha\" TEXT NOT NULL)",
                "CREATE TABLE \"Status\" (\"status_id\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\"status_jogo\" TEXT NOT NULL,\"status_tipo\" INTEGER NOT NULL)" };

        public const string localDB = "configDia.db";

    }
}
