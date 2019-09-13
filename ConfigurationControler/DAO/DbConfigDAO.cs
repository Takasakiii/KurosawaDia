using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;

namespace ConfigurationControler.DAO
{
    public class DbConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public DBConfig GetDbConfig()
        {
            SqliteCommand cmd = conexao.CreateCommand();
            cmd.CommandText = "select * from DbConfig";

            using (SqliteDataReader rs = cmd.ExecuteReader())
            {
                DBConfig dbConfig = null;
                while (rs.Read())
                {
                    if (rs["porta"] != DBNull.Value)
                    {
                        dbConfig = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], Convert.ToInt32(rs["porta"]));
                    }
                    else
                    {
                        dbConfig = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], null);
                    }
                }
                rs.Close();
                conexao.Close();
                return dbConfig;
            }
        }
    }
}
