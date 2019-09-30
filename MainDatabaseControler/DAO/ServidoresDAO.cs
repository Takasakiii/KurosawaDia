using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using static MainDatabaseControler.Modelos.Servidores;

namespace MainDatabaseControler.DAO
{
    public class ServidoresDAO
    {
        private MySqlConnection conexao = null;

        public ServidoresDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool GetPrefix(ref Servidores servidor)
        {
            const string sql = "call buscarPrefix(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            string prefix = null;
            if (rs.Read())
            {
                prefix = rs["prefix_servidor"].ToString();
            }

            bool retorno = false;
            if (!string.IsNullOrEmpty(prefix))
            {
                servidor = new Servidores(servidor.Id, prefix.ToCharArray());
                retorno = true;
            }

            rs.Close();
            conexao.Close();
            return retorno;
        }

        public bool SetServidorPrefix(ref Servidores servidor)
        {
            const string sql = "call atualizarPrefix(@id, @prefix)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.Id);
            cmd.Parameters.AddWithValue("@prefix", new string(servidor.Prefix));

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retono = false;
            if (rs.Read())
            {
                servidor = new Servidores(servidor.Id, ((string)rs["prefix_servidor"]).ToCharArray());
                retono = true;
            }
            rs.Close();
            conexao.Close();
            return retono;
        }

        public bool GetPermissoes(ref Servidores servidor)
        {
            const string sql = "call GetPermissoes(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                servidor = new Servidores(servidor.Id, (PermissoesServidores)rs["especial_servidor"], servidor.Prefix, servidor.Nome);
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }

        public bool SetEspecial(Servidores servidor)
        {
            const string sql = "call DefinirTipoServidor(@id, @tipo)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", servidor.Id);
            cmd.Parameters.AddWithValue("@tipo", (int)servidor.Permissoes);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                retorno = Convert.ToBoolean(rs["Result"]);
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }
    }
}
