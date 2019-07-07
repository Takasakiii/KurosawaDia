using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace Bot.DataBase.MainDB.DAO
{
    public class ServidoresDAO
    {
        private MySqlConnection conexao;

        public ServidoresDAO()
        {
            conexao = new MySqlConstructor().Conectar();
        }

        public void inserirServidorUsuario(Servidores servidor, Usuarios usuario)
        {
            const string sql = "call inserirServidor_Usuario(@sid, @snome, @uid, @unome)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@sid", servidor.id);
            cmd.Parameters.AddWithValue("@snome", servidor.nome);
            cmd.Parameters.AddWithValue("@uid", usuario.id);
            cmd.Parameters.AddWithValue("@unome", usuario.nome);

            cmd.ExecuteNonQuery();
            conexao.Close();            
        }

        public char[] GetPrefix(Servidores servidor)
        {
            const string sql = "buscarPrefix(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            char[] prefix = null;
            if (rs.Read())
            {
                prefix = rs["prefix_servidor"].ToString().ToCharArray();
            }
            rs.Close();
            conexao.Close();
            return prefix;
        }
    }
}
