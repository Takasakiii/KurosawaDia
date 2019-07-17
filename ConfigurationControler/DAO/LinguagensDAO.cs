using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigurationControler.DAO
{
    public class LinguagensDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public void Adicionar (Linguagens linguagens)
        {
            conexao.Open();
            AdicionarTabela();
            const string sql = "insert into Linguagens (idiomaString, stringIdentifier, String) values (@idioma, @id, @txt)";
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
            cmd.Parameters.AddWithValue("@id", linguagens.stringIdentifier);
            cmd.Parameters.AddWithValue("@txt", linguagens.texto);
            cmd.ExecuteNonQuery();
            conexao.Close();
        }


        private void AdicionarTabela()
        {
            SqliteCommand cmd = new SqliteCommand(DB.sqlCriacao[4], conexao);
            cmd.ExecuteNonQuery();
        }

        public Tuple<bool, List<Linguagens>> Listar(Linguagens linguagens)
        {
            conexao.Open();
            AdicionarTabela();
            const string sql = "select * from Linguagens where idiomaString=@idioma";
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
            SqliteDataReader rs = cmd.ExecuteReader();
            List<Linguagens> resul = new List<Linguagens>();
            bool result = false;
            while (rs.Read())
            {
                if(rs["idString"] != null)
                {
                    result = true;
                    Linguagens temp = new Linguagens((Linguagens.Idiomas)Convert.ToInt32(rs["idiomaString"]), (string)rs["stringIdentifier"], (string)rs["String"], Convert.ToUInt32(rs["idString"]));
                    resul.Add(temp);
                }
            }
            return Tuple.Create(result, resul);
        }

        public void Deletar(Linguagens linguagens)
        {
            conexao.Open();
            const string sql = "delete from Linguagens where idString = @id";
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@id", linguagens.idString);
            cmd.ExecuteNonQuery();
            conexao.Close();
        }

        public void Atualisar(Linguagens linguagens)
        {
            conexao.Open();
            const string sql = "update Linguagens set idiomaString = @idioma, stringIdentifier = @id, String = @txt where idString = @cod";
            SqliteCommand cmd = new SqliteCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
            cmd.Parameters.AddWithValue("@id", linguagens.stringIdentifier);
            cmd.Parameters.AddWithValue("@txt", linguagens.texto);
            cmd.Parameters.AddWithValue("@cod", linguagens.idString);
            cmd.ExecuteNonQuery();
            conexao.Close();
        }
    }
}
