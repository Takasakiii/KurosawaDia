using MySql.Data.MySqlClient;

namespace Bot.DataBase.Constructors
{
    class MySqlConstructor
    {
        public MySqlConnection Conectar()
        {
            MySqlConnection conexao = new MySqlConnection("Server=127.0.0.1;Database=Ayura;Uid=ayura;Pwd=xpto20166102;");
            conexao.Open();
            return conexao;
        }
    }
}
