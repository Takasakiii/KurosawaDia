using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace Bot.DataBase.MainDB.DAO
{
    public class FuckDAO
    {
        private MySqlConnection conexao = null;
        public FuckDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public bool GetImg(ref Fuck fuck)
        {
            const string sql = "call GetFuckImg(@explicit)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@explicit", fuck.explicitImg);

            bool retorno = false;

            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                Usuarios usuario = new Usuarios(Convert.ToUInt64(rs["id_usuario"]), (string)rs["nome_usuario"]);
                fuck.SetImg(Convert.ToBoolean(rs["explicitImage"]), (string)rs["urlImage"], usuario, Convert.ToUInt32(rs["cod"]));
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }

        public void AddImg(Fuck fuck)
        {
            const string sql = "call AdicionarImgFuck(@id, @img, @explicit)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", fuck.usuario.id);
            cmd.Parameters.AddWithValue("@img", fuck.img);
            cmd.Parameters.AddWithValue("@explicit", fuck.explicitImg);

            cmd.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
