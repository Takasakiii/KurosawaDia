using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ConfigurationControler.Factory
{
    public class ConnectionFactory
    {

        internal static async Task ConectarAsync(Action<SqliteConnection> funcaoDados)
        {
            if (!File.Exists(DB.localDB))
            {
                await CriarDBAsync();
            }

            SqliteConnection conexao = new SqliteConnection($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{DB.localDB}");
            await conexao.OpenAsync();
            funcaoDados.Invoke(conexao);
            await conexao.CloseAsync();
        }

        private static async Task CriarDBAsync()
        {
            FileStream fs = File.Create(DB.localDB);
            fs.Close();

            await ConectarAsync(async (SqliteConnection) =>
            {
                for (int i = 0; i < DB.sqlCriacao.Length; i++)
                {
                    SqliteCommand cmd = new SqliteCommand(DB.sqlCriacao[i], SqliteConnection);
                    await cmd.ExecuteNonQueryAsync();
                }
            });
        }

        public static bool VerificarDB()
        {
            return File.Exists(DB.localDB);
        }
    }
}
