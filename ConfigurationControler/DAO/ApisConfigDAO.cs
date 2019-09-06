using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace ConfigurationControler.DAO
{
    public class ApisConfigDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public Tuple<bool, List<ApisConfig>> Carregar()
        {
            SqliteCommand selectCmd = conexao.CreateCommand();
            selectCmd.CommandText = "select * from ApisConfig";
            bool result = true; //fake
            List<ApisConfig> retorno = new List<ApisConfig>();
            try
            {
                using (SqliteDataReader reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        ApisConfig temp = new ApisConfig((string)reader["ApiIdentifier"], (string)reader["Token"], Convert.ToBoolean(reader["Ativada"]), Convert.ToUInt32(reader["id"]));
                        retorno.Add(temp);
                    }
                    reader.Close();
                }
            }
            catch
            {
                result = false;
            }
            finally
            {
                conexao.Close();
            }
            return Tuple.Create(result, retorno);
        }

        
    }
}
