using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;

namespace Bot.DataBase.MainDB.DAO
{
    public class ACRsDAO
    {
        private MySqlConnection conexao;

        public ACRsDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public ACRs ResponderAcr(ACRs acr)
        {
            const string sql = "call responderACR(@trigger, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@trigger", acr.trigger);
            cmd.Parameters.AddWithValue("@id", acr.id_servidor);

            MySqlDataReader rs = cmd.ExecuteReader();
            ACRs tmp = new ACRs();
            if (rs.Read())
            {
                tmp.SetResposta(rs["resposta_acr"].ToString());
            }
            rs.Close();
            conexao.Close();
            return tmp;
        }
    }
}
