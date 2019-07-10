using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Data.Sqlite;
using ConfigurationControler.Singletons;

namespace ConfigurationControler.Factory
{
    public class ConnectionFactory
    {

        public SqliteConnection Conectar()
        {
            if (!File.Exists(DB.LocalFile))
            {
                CriarDB();
            }
            return new SqliteConnection($"Data Source={DB.LocalFile}");
        }

        private void CriarDB()
        {
            try
            {
                FileStream fs = File.Create(DB.LocalFile);
                fs.Close();

                ConnectionFactory repetidor = new ConnectionFactory();
                SqliteConnection conexao = repetidor.Conectar();


                string[] tabelas = { "CREATE TABLE \"ApiConfig\" (\"id\"INTEGER PRIMARY KEY AUTOINCREMENT,\"WeebToken\" TEXT NOT NULL)",
                "CREATE TABLE \"DiaConfig\" (\"id\" INTEGER PRIMARY KEY AUTOINCREMENT,\"token\" TEXT NOT NULL,\"prefix\" TEXT,\"idDono\" INTEGER NOT NULL)",
                "CREATE TABLE \"DbConfig\" (\"id\"    INTEGER PRIMARY KEY AUTOINCREMENT,\"ip\"    TEXT NOT NULL,\"database\"  TEXT NOT NULL,\"login\" TEXT NOT NULL,\"senha\" TEXT NOT NULL)",
                "CREATE TABLE \"Status\" (\"status_id\" INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,\"status_jogo\" TEXT NOT NULL,\"status_tipo\" INTEGER NOT NULL)" };
                conexao.Open();
                for (int i = 0; i < tabelas.Length; i++)
                {
                    SqliteCommand cmd = new SqliteCommand(tabelas[i], conexao);
                    cmd.ExecuteNonQuery();
                }

                conexao.Close();
            }
            catch
            {
                throw;
            }
        }
    }
}
