using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;

namespace MainDatabaseControler.DAO
{
    public class ReacoesCustomizadasDAO
    {
        public async Task<Tuple<bool, ReacoesCustomizadas>> ResponderAcrAsync(ReacoesCustomizadas rc)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call responderACR(@trigger, @id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@trigger", rc.Trigger);
                cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    rc.SetResposta((string)rs["resposta_acr"]);
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, rc);
        }

        public async Task<Tuple<bool, ReacoesCustomizadas>> CriarAcrAsync(ReacoesCustomizadas rc)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call criarAcr(@trigger, @resposta, @id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@trigger", rc.Trigger);
                cmd.Parameters.AddWithValue("@resposta", rc.Resposta);
                cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    rc = new ReacoesCustomizadas(Convert.ToUInt64(rs["codigo_acr"]));
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, rc);
        }

        public async Task<bool> DeletarAcrAsync(ReacoesCustomizadas rc)
        {
            bool result = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call deletarAcr(@codigo, @id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@codigo", rc.Cod);
                cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    result = Convert.ToBoolean(rs["Result"]);
                }
            });
            return result;
        }

        public async Task<List<ReacoesCustomizadas>> ListarAcrAsync(ReacoesCustomizadas rc)
        {
            List<ReacoesCustomizadas> acrsTmp = new List<ReacoesCustomizadas>();
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call listarAcr(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", rc.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                while (await rs.ReadAsync())
                {
                    ReacoesCustomizadas tmp = new ReacoesCustomizadas((string)rs["trigger_acr"], (string)rs["resposta_acr"], rc.Servidor, Convert.ToUInt64(rs["codigo_acr"]));
                    acrsTmp.Add(tmp);
                }
            });
            return acrsTmp;
        }
    }
}
