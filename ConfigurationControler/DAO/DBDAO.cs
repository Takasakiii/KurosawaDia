using ConfigurationControler.Factory;
using ConfigurationControler.Modelos;
using ConfigurationControler.Singletons;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;

namespace ConfigurationControler.DAO
{
    public class DBDAO
    {
        private SqliteConnection conexao = new ConnectionFactory().Conectar();

        public void AdicionarAtualizar(ApisConfig[] apiConfig, DBConfig dBConfig, DiaConfig diaConfig)
        {
            string[] sqls = { "drop TABLE if EXISTS ApisConfig;", "drop TABLE if EXISTS DiaConfig;", "drop TABLE if EXISTS DbConfig;" };

            conexao.Open();
            for (int i = 0; i < sqls.Length; i++)
            {
                SqliteCommand cmd = new SqliteCommand(sqls[i], conexao);
                cmd.ExecuteNonQuery();
                cmd = new SqliteCommand(DB.sqlCriacao[i], conexao);
                cmd.ExecuteNonQuery();
            }

            sqls[0] = "insert into ApisConfig values (@idapi, @ApiIdentifier, @Token, @Ativada), (@idapi2, @ApiIdentifier2, @Token2, @Ativada2);";
            sqls[1] = "insert into DbConfig values (1, @ip, @database, @login, @senha, @porta);";
            sqls[2] = "insert into DiaConfig values (1, @tk, @pr, @id);";

            SqliteCommand cmda = new SqliteCommand(sqls[0], conexao);
            cmda.Parameters.AddWithValue("@idapi", apiConfig[0].id);
            cmda.Parameters.AddWithValue("@ApiIdentifier", apiConfig[0].ApiIdentifier);
            cmda.Parameters.AddWithValue("@Token", apiConfig[0].Token);
            cmda.Parameters.AddWithValue("@Ativada", apiConfig[0].Ativada);
            cmda.Parameters.AddWithValue("@idapi2", apiConfig[1].id);
            cmda.Parameters.AddWithValue("@ApiIdentifier2", apiConfig[1].ApiIdentifier);
            cmda.Parameters.AddWithValue("@Token2", apiConfig[1].Token);
            cmda.Parameters.AddWithValue("@Ativada2", apiConfig[1].Ativada);
            //cmda.Parameters.AddWithValue("@dblToken", apiConfig.dblToken);
            //cmda.Parameters.AddWithValue("@atualizarDbl", apiConfig.atualizarDbl);
            cmda.ExecuteNonQuery();
            cmda = new SqliteCommand(sqls[1], conexao);
            cmda.Parameters.AddWithValue("@ip", dBConfig.ip);
            cmda.Parameters.AddWithValue("@database", dBConfig.database);
            cmda.Parameters.AddWithValue("@login", dBConfig.login);
            cmda.Parameters.AddWithValue("@senha", dBConfig.senha);
            if(dBConfig.porta != null)
            {
                cmda.Parameters.AddWithValue("@porta", dBConfig.porta);
            }
            else
            {
                cmda.Parameters.AddWithValue("@porta", DBNull.Value);
            }
            cmda.ExecuteNonQuery();
            cmda = new SqliteCommand(sqls[2], conexao);
            cmda.Parameters.AddWithValue("@tk", diaConfig.token);
            cmda.Parameters.AddWithValue("@pr", diaConfig.prefix);
            cmda.Parameters.AddWithValue("@id", diaConfig.idDono);
            cmda.ExecuteNonQuery();
            conexao.Close();

        }

        public Tuple<int, List<ApisConfig>, DBConfig, DiaConfig> PegarDadosBot()
        {
            string[] sqls = { "select * from ApisConfig", "select * from DbConfig", "select * from DiaConfig" };
            conexao.Open();

            int estado = 0;
            List<ApisConfig> apis = new List<ApisConfig>();
            DBConfig db = null;
            DiaConfig dia = null;

            try
            {
                SqliteCommand cmd = new SqliteCommand(sqls[0], conexao);
                SqliteDataReader rs = cmd.ExecuteReader();
                while (rs.Read())
                {
                    ApisConfig temp = new ApisConfig((string)rs["ApiIdentifier"], (string)rs["Token"], Convert.ToBoolean(rs["Ativada"]), Convert.ToUInt32(rs["id"]));
                    apis.Add(temp);
                    
                }
                estado++;
                cmd = new SqliteCommand(sqls[1], conexao);
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    if(rs["porta"] != DBNull.Value)
                    {
                        db = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], Convert.ToInt32(rs["porta"]));
                    }
                    else
                    {
                        db = new DBConfig((string)rs["ip"], (string)rs["database"], (string)rs["login"], (string)rs["senha"], null);
                    }
                    estado++;
                }
                cmd = new SqliteCommand(sqls[2], conexao);
                rs = cmd.ExecuteReader();
                if (rs.Read())
                {
                    dia = new DiaConfig((string)rs["token"], (string)rs["prefix"], Convert.ToUInt64(rs["idDono"]));
                    estado++;
                }

            }
            catch
            {
                estado = 0;
            }
            finally
            {
                conexao.Close();
            }
            return Tuple.Create(estado, apis, db, dia);
        }

    }
}
