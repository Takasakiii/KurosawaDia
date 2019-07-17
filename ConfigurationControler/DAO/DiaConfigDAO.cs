using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;

namespace ConfigurationControler.DAO
{
    public class DiaConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public DiaConfig Carregar()
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from DiaConfig where id = @id";
            selectCmd.Parameters.AddWithValue("@id", DiaConfig.id);

            using (SqliteDataReader reader = selectCmd.ExecuteReader())
            {
                DiaConfig diaConfig = null;
                if (reader.Read())
                {
                    diaConfig = new DiaConfig(reader.GetString(reader.GetOrdinal("token")), reader.GetString(reader.GetOrdinal("prefix")), Convert.ToUInt64(reader["idDono"]));
                }
                reader.Close();
                conexao.Close();
                return diaConfig;
            }
        }
    }
}
