using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace MainDatabaseControler.Factory
{
    public class ConnectionFactory
    {
        public MySqlConnection Conectar(Action funcao)
        {
            DBConfig dbConfig = new DbConfigDAO().GetDbConfig();
            string stringConexao = $"Server={dbConfig.ip};Database={dbConfig.database};Uid={dbConfig.login};Pwd={dbConfig.senha};";

            if(dbConfig.porta != null)
            {
                stringConexao += $"Port={dbConfig.porta};";
            }

            MySqlConnection conexao = new MySqlConnection(stringConexao);
            conexao.Open();
            return conexao;
        }
    }
}
