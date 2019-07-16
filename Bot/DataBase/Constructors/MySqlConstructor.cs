using ConfigurationControler.DAO;
using ConfigurationControler.Modelos;
using MySql.Data.MySqlClient;

namespace Bot.DataBase.Constructors
{
    public class MySqlConstructor
    {
        public MySqlConnection Conectar()
        {
            DBConfig dbConfig = new DbConfigDAO().GetDbConfig();
            MySqlConnection conexao = new MySqlConnection($"Server={dbConfig.ip};Database={dbConfig.database};Uid={dbConfig.login};Pwd={dbConfig.senha};");
            conexao.Open();
            return conexao;
        }
    }
}
