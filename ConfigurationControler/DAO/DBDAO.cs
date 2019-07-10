using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.DAO
{
    public class DBDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public void AdicionarAtualizar(ApiConfig apiConfig, DBConfig dBConfig, DiaConfig diaConfig)
        {
            string[] sqls = { "drop TABLE if EXISTS ApiConfig;", "drop TABLE if EXISTS DiaConfig;", "drop TABLE if EXISTS DbConfig;" };

            conexao.Open();
            for(int i = 0; i < sqls.Length; i++)
            {
                SqliteCommand cmd = new SqliteCommand(sqls[i], conexao);
                cmd.ExecuteNonQuery();
                cmd = new SqliteCommand(DB.sqlCriacao[i], conexao);
                cmd.ExecuteNonQuery();
            }

            sqls[0] = "insert into ApiConfig values (1, @tk);";
            sqls[1] = "insert into DbConfig values (1, @ip, @database, @login, @senha);";
            sqls[2] = "insert into DiaConfig values (1, @tk, @pr, @id);";

            SqliteCommand cmda = new SqliteCommand(sqls[0], conexao);
            cmda.Parameters.AddWithValue("@tk", apiConfig.WeebToken);
            cmda.ExecuteNonQuery();
            cmda = new SqliteCommand(sqls[1], conexao);
            cmda.Parameters.AddWithValue("@ip", dBConfig.ip);
            cmda.Parameters.AddWithValue("@database", dBConfig.database);
            cmda.Parameters.AddWithValue("@login", dBConfig.login);
            cmda.Parameters.AddWithValue("@senha", dBConfig.senha);
            cmda.ExecuteNonQuery();
            cmda = new SqliteCommand(sqls[2], conexao);
            cmda.Parameters.AddWithValue("@tk", diaConfig.token);
            cmda.Parameters.AddWithValue("@pr", diaConfig.prefix);
            cmda.Parameters.AddWithValue("@id", diaConfig.idDono);
            cmda.ExecuteNonQuery();
            conexao.Close();

        }


        
    }
}
