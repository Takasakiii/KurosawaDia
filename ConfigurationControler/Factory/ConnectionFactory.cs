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
            if (!File.Exists(DB.localDB))
            {
                CriarDB();
            }
            return new SqliteConnection($"Data Source={DB.localDB}");
        }

        private void CriarDB()
        {
            try
            {
                FileStream fs = File.Create(DB.localDB);
                fs.Close();

                ConnectionFactory repetidor = new ConnectionFactory();
                SqliteConnection conexao = repetidor.Conectar();


                
                conexao.Open();
                for (int i = 0; i < DB.sqlCriacao.Length; i++)
                {
                    SqliteCommand cmd = new SqliteCommand(DB.sqlCriacao[i], conexao);
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
