using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;

namespace Bot.DataBase.MainDB.DAO
{
    public class CanaisDAO
    {
        private MySqlConnection conexao = null;

        public CanaisDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public bool AdcCh(Canais canal)
        {
            try
            {
                const string sql = "call AdcCh(@tipo, @canal, @id_canal, @id_servidor)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@tipo", canal.tipoCanal);
                cmd.Parameters.AddWithValue("@canal", canal.canal);
                cmd.Parameters.AddWithValue("@id_canal", canal.id);
                cmd.Parameters.AddWithValue("@id_servidor", canal.servidor.id);

                cmd.ExecuteNonQuery();
                conexao.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
