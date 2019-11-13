using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConfigurationControler.DAO
{
    public class LinguagensDAO
    {

        public async Task AdicionarAsync (Linguagens linguagens)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                await AdicionarTabelaAsync(conexao);
                const string sql = "insert into Linguagens (idiomaString, stringIdentifier, String) values (@idioma, @id, @txt)";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
                cmd.Parameters.AddWithValue("@id", linguagens.stringIdentifier);
                cmd.Parameters.AddWithValue("@txt", linguagens.texto);
                await cmd.ExecuteNonQueryAsync();
            });
            
        }


        private async Task AdicionarTabelaAsync(SqliteConnection conexao)
        {
            SqliteCommand cmd = new SqliteCommand(DB.sqlCriacao[4], conexao);
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task<Tuple<bool, Linguagens[]>> ListarAsync(Linguagens linguagens)
        {
            List<Linguagens> resul = new List<Linguagens>();
            bool result = false;

            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                await AdicionarTabelaAsync(conexao);
                const string sql = "select * from Linguagens where idiomaString=@idioma";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
                SqliteDataReader rs = await cmd.ExecuteReaderAsync();

                while (await rs.ReadAsync())
                {
                    if (rs["idString"] != null)
                    {
                        result = true;
                        Linguagens temp = new Linguagens((Linguagens.Idiomas)Convert.ToInt32(rs["idiomaString"]), (string)rs["stringIdentifier"], (string)rs["String"], Convert.ToUInt32(rs["idString"]));
                        resul.Add(temp);
                    }
                }

            });

            return Tuple.Create(result, resul.ToArray());
        }

        public async Task DeletarAsync(Linguagens linguagens)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "delete from Linguagens where idString = @id";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@id", linguagens.idString);
                await cmd.ExecuteNonQueryAsync();
            });
            
        }

        public async Task AtualisarAsync(Linguagens linguagens)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "update Linguagens set idiomaString = @idioma, stringIdentifier = @id, String = @txt where idString = @cod";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@idioma", (int)linguagens.idiomaString);
                cmd.Parameters.AddWithValue("@id", linguagens.stringIdentifier);
                cmd.Parameters.AddWithValue("@txt", linguagens.texto);
                cmd.Parameters.AddWithValue("@cod", linguagens.idString);
                await cmd.ExecuteNonQueryAsync();
            });
        }

        public async Task<Tuple<bool, Linguagens>> GetStringAsync (Linguagens linguagens)
        {
            bool estado = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "select idString, String from Linguagens where idiomaString = @idioma and stringIdentifier = @id";
                SqliteCommand cmd = new SqliteCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@idioma", linguagens.idiomaString);
                cmd.Parameters.AddWithValue("@id", linguagens.stringIdentifier);
                SqliteDataReader rs = await cmd.ExecuteReaderAsync();

                if (await rs.ReadAsync())
                {
                    if (rs["idString"] != null)
                    {
                        estado = true;
                        linguagens.SetString(Convert.ToUInt64(rs["idString"]), (string)rs["String"]);
                    }
                }
            });

            return Tuple.Create(estado, linguagens);
        }
    }
}
