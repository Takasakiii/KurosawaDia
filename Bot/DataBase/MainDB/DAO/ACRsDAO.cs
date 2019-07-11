using Bot.DataBase.Constructors;
using Bot.DataBase.MainDB.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

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
            cmd.Parameters.AddWithValue("@id", acr.servidores.id);

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

        public ulong CriarAcr(ACRs acr)
        {
            const string sql = "call criarAcr(@trigger, @resposta, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@trigger", acr.trigger);
            cmd.Parameters.AddWithValue("@resposta", acr.resposta);
            cmd.Parameters.AddWithValue("@id", acr.servidores.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            ulong codigo = 0;
            if (rs.Read())
            {
                codigo = Convert.ToUInt64(rs["codigo_acr"]);
            }
            rs.Close();
            conexao.Close();
            return codigo;
        }

        public bool DeletarAcr(ACRs acr)
        {
            const string sql = "call deletarAcr(@codigo, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@codigo", acr.codigo);
            cmd.Parameters.AddWithValue("@id", acr.servidores.id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool result = false;
            if (rs.Read())
            {
                result = Convert.ToBoolean(rs["Result"]);
            }
            rs.Close();
            conexao.Close();
            return result;
        }

        public List<ACRs> ListarAcr(ACRs acr)
        {
            const string sql = "call listarAcr(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", acr.servidores.id);
            List<ACRs> acrsTmp = new List<ACRs>();

            MySqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                ACRs tmp = new ACRs();
                tmp.SetAcr(rs["trigger_acr"].ToString(), rs["resposta_acr"].ToString(), new Servidores(acr.servidores.id, Convert.ToUInt64(rs["codigo_server"])), Convert.ToUInt64(rs["codigo_acr"]));
                acrsTmp.Add(tmp);
            }
            rs.Close();
            conexao.Close();
            return acrsTmp;
        }
    }
}
