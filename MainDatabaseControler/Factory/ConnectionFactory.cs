using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Threading.Tasks;

namespace MainDatabaseControler.Factory
{
    internal class ConnectionFactory
    {
        internal static async Task ConectarAsync(Action<MySqlConnection> funcao)
        {
            DBConfig dbConfig = await new DbConfigDAO().GetDbConfigAsync();
            string stringConexao = $"Server={dbConfig.ip};Database={dbConfig.database};Uid={dbConfig.login};Pwd={dbConfig.senha};";

            if(dbConfig.porta != null)
            {
                stringConexao += $"Port={dbConfig.porta};";
            }

            MySqlConnection conexao = new MySqlConnection(stringConexao);
            await conexao.OpenAsync();
            funcao.Invoke(conexao);
            await conexao.CloseAsync();
        }
    }
}
