using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ConfigurationControler.DAO
{
    public class ApisConfigDAO
    {

        public async Task<ApisConfig[]> CarregarAsync()
        {
            List<ApisConfig> retorno = new List<ApisConfig>();
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                SqliteCommand selectCmd = conexao.CreateCommand();
                selectCmd.CommandText = "select * from ApisConfig";
                using (SqliteDataReader reader = await selectCmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ApisConfig temp = new ApisConfig((string)reader["ApiIdentifier"], (string)reader["Token"], Convert.ToBoolean(reader["Ativada"]), Convert.ToUInt32(reader["id"]));
                        retorno.Add(temp);
                    }
                    reader.Close();
                }
            });

            return retorno.ToArray();
        }


           

        
    }
}
