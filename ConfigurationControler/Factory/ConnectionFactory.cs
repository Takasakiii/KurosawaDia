using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System.IO;

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
            SqliteConnection conexao = new SqliteConnection($"Data Source={DB.localDB}");
            conexao.Open();
            return conexao;
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
