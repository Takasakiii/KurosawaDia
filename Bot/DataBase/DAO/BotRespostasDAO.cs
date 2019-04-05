using Bot.Configs.Modelos;
using Bot.Constructor;
using Bot.Modelos.Objetos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bot.DataBase.DAO
{
    public class BotRespostasDAO
    {
        private MySqlConnection connection;
        
        public BotRespostasDAO(DBconfig dbConf)
        {
            connection = new MySqlConstructor().Conectar(dbConf);
        }

        public BotRespostas Add (BotRespostas resp)
        {
            MySqlCommand cmd = new MySqlCommand("call inserirACR(@pergunta, @resposta, @sid);", connection);
            cmd.Parameters.AddWithValue("@pergunta", resp.pergunta);
            cmd.Parameters.AddWithValue("@resposta", resp.resposta);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            while(rs.Read())
            {
                resp.id = (long)rs["id"];
            }
            connection.Close();
            return resp;
        }

        public BotRespostas Responder (BotRespostas resp)
        {
            MySqlCommand cmd = new MySqlCommand("call responderACR(@sid, @perg);", connection);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);
            cmd.Parameters.AddWithValue("@perg", resp.pergunta);

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            while(rs.Read())
            {
                resp.resposta = rs["resposta_resposta"].ToString();
            }
            connection.Close();
            return resp;
        }

        public List<BotRespostas> Listar(BotRespostas resp)
        {
            MySqlCommand cmd = new MySqlCommand("call listarACR(@sid);", connection);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            List<BotRespostas> ret = new List<BotRespostas>();
            while(rs.Read())
            {
                BotRespostas temp = new BotRespostas();
                temp.id = (long)rs["resposta_id"];
                temp.pergunta = rs["resposta_pergunta"].ToString();
                temp.resposta = rs["resposta_resposta"].ToString();
                ret.Add(temp);
            }
            connection.Close();
            return ret;
        }

        public List<BotRespostas> Procurar (BotRespostas resp)
        {
            MySqlCommand cmd = new MySqlCommand("call procurarACR(@sid, @p);", connection);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);
            cmd.Parameters.AddWithValue("@p", $"%{resp.pergunta}%");

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            List<BotRespostas> ret = new List<BotRespostas>();
            while(rs.Read())
            {
                BotRespostas temp = new BotRespostas();
                temp.id = (long)rs["resposta_id"];
                temp.pergunta = rs["resposta_pergunta"].ToString();
                temp.resposta = rs["resposta_resposta"].ToString();
                ret.Add(temp);
            }
            connection.Close();
            return ret;
        }

        public bool Deletar(BotRespostas resp)
        {
            MySqlCommand cmd = new MySqlCommand("call deletarACR(@rid, @sid)", connection);
            cmd.Parameters.AddWithValue("@rid", resp.id);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            long result = 0;
            while (rs.Read())
            {
                result = (long)rs[0];
            }
            connection.Close();
            if (result == 0)
                return false;
            else
                return true;
        }

        public bool Atualizar(int opc, BotRespostas resp, string arg)
        {
            MySqlCommand cmd = new MySqlCommand("call atualizarACR(@rid, @sid, @opc, @arg);", connection);
            cmd.Parameters.AddWithValue("@rid", resp.id);
            cmd.Parameters.AddWithValue("@sid", resp.servidor.id);
            cmd.Parameters.AddWithValue("@opc", opc);
            cmd.Parameters.AddWithValue("@arg", arg);

            connection.Open();
            MySqlDataReader rs = cmd.ExecuteReader();
            long result = 0;
            while (rs.Read())
            {
                result = (long)rs[0];
            }
            connection.Close();
            if (result == 0)
                return false;
            else
                return true;
        }
    }
}
