using Bot.Constructors;
using Bot.Modelos;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;

namespace Bot.DAO
{
    public class AyuraConfigDAO
    {
        private SQLiteConnection conexao = new SQLiteConstrutor().Conectar();

        public AyuraConfig Carregar(AyuraConfig ayuraConfig)
        {
            using(SQLiteCommand cmd = new SQLiteCommand(conexao))
            {
                cmd.CommandText = "select * from AyuraConfig where id = @id";
                cmd.Parameters.AddWithValue("@id", ayuraConfig.id);
                SQLiteDataReader rs = cmd.ExecuteReader();
                if(rs.Read())
                {
                    ayuraConfig.SetBotConfig((string)rs["Token"], (string)rs["Prefix"], (string)rs["idDono"]);
                }
                conexao.Close();
                return ayuraConfig;
            }
        }
    }
}
