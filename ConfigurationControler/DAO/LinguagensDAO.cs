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
    }
}
