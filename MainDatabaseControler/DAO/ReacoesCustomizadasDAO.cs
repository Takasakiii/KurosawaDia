using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;

namespace MainDatabaseControler.DAO
{
    public class ReacoesCustomizadasDAO
    {
        private MySqlConnection conexao = null;

        public ReacoesCustomizadasDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool ResponderAcr(ref ReacoesCustomizadas rc)
        {
            const string sql = "call responderACR(@trigger, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@trigger", rc.Trigger);
            cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                rc.SetResposta((string)rs["resposta_acr"]);
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }

        public bool CriarAcr(ref ReacoesCustomizadas rc)
        {
            const string sql = "call criarAcr(@trigger, @resposta, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@trigger", rc.Trigger);
            cmd.Parameters.AddWithValue("@resposta", rc.Resposta);
            cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            bool retorno = false;
            if (rs.Read())
            {
                rc = new ReacoesCustomizadas(Convert.ToUInt64(rs["codigo_acr"]));
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return retorno;
        }

        public bool DeletarAcr(ReacoesCustomizadas rc)
        {
            const string sql = "call deletarAcr(@codigo, @id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@codigo", rc.Cod);
            cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

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

        public List<ReacoesCustomizadas> ListarAcr(ReacoesCustomizadas rc)
        {
            const string sql = "call listarAcr(@id)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);
            List<ReacoesCustomizadas> acrsTmp = new List<ReacoesCustomizadas>();

            MySqlDataReader rs = cmd.ExecuteReader();
            while (rs.Read())
            {
                ReacoesCustomizadas tmp = new ReacoesCustomizadas((string)rs["trigger_acr"], (string)rs["resposta_acr"], rc.Servidor,  Convert.ToUInt64(rs["codigo_acr"]));
                acrsTmp.Add(tmp);
            }
            rs.Close();
            conexao.Close();
            return acrsTmp;
        }
    }
}
