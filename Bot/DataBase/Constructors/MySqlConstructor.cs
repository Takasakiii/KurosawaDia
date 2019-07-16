using ConfigurationControler.ConfigDB.DAO;
using ConfigurationControler.ConfigDB.Modelos;
using MySql.Data.MySqlClient;

namespace Bot.DataBase.Constructors
{
    public class MySqlConstructor
    {
        public MySqlConnection Conectar()
        {
            DbConfig dbConfig = new DbConfigDAO().GetDbConfig();
            MySqlConnection conexao = new MySqlConnection($"Server={dbConfig.ip};Database={dbConfig.database};Uid={dbConfig.login};Pwd={dbConfig.senha};");
            conexao.Open();
            return conexao;
        }
    }
}
