namespace ConfigurationControler.Singletons
{
    public static class DB
    {

        public readonly static string[] sqlCriacao = { "CREATE TABLE IF NOT EXISTS \"ApisConfig\" (\"id\"INTEGER PRIMARY KEY, \"ApiIdentifier\" TEXT NOT NULL UNIQUE,\"Token\" TEXT NOT NULL, \"Ativada\"INTEGER NOT NULL)",
                "CREATE TABLE \"DiaConfig\" (\"id\" INTEGER PRIMARY KEY,\"token\" TEXT NOT NULL,\"prefix\" TEXT,\"idDono\" INTEGER NOT NULL)",
                "CREATE TABLE \"DbConfig\" (\"id\"    INTEGER PRIMARY KEY,\"ip\"    TEXT NOT NULL,\"database\"  TEXT NOT NULL,\"login\" TEXT NOT NULL,\"senha\" TEXT NOT NULL, \"porta\" INTEGER)",
                "CREATE TABLE \"Status\" (\"status_id\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\"status_jogo\" TEXT NOT NULL, \"status_url\" TEXT, \"status_tipo\" INTEGER NOT NULL)",
                "CREATE TABLE IF NOT EXISTS \"Linguagens\" (\"idString\"  INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, \"idiomaString\"  INTEGER NOT NULL, \"stringIdentifier\"  TEXT NOT NULL, \"String\" TEXT NOT NULL)"};

        public const string localDB = "configDia.db";

    }
}
