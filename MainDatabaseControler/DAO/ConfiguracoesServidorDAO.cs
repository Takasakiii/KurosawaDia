using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace MainDatabaseControler.DAO
{
    public class ConfiguracoesServidorDAO
    {
        public async Task<bool> SalvarPIConfigAsync(ConfiguracoesServidor config)
        {
            bool retorno = true;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call configurePI(@id, @enable, @pirate, @msg)";
                try
                {
                    MySqlCommand cmd = new MySqlCommand(sql, conexao);
                    cmd.Parameters.AddWithValue("@id", config.servidor.Id);
                    cmd.Parameters.AddWithValue("@enable", config.pI.PIConf);
                    cmd.Parameters.AddWithValue("@pirate", config.pI.PIRate);
                    if (!string.IsNullOrEmpty(config.pI.MsgPIUp))
                    {
                        cmd.Parameters.AddWithValue("@msg", config.pI.MsgPIUp);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@msg", DBNull.Value);
                    }

                    await cmd.ExecuteNonQueryAsync();
                }
                catch
                {
                    retorno = false;
                    throw;
                }
            });
            return retorno;
        }

        public async Task SetWelcomeMsgAsync(ConfiguracoesServidor configuracoes)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call SetWelcomeMsg(@id, @msg)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
                if (!string.IsNullOrEmpty(configuracoes.bemvindo.bemvindoMsg))
                {
                    cmd.Parameters.AddWithValue("@msg", configuracoes.bemvindo.bemvindoMsg);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@msg", DBNull.Value);
                }

                await cmd.ExecuteNonQueryAsync();
            });
        }

        public async Task<Tuple<bool, ConfiguracoesServidor>> GetWelcomeMsgAsync(ConfiguracoesServidor configuracoes)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetWelcomeMsg(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    if (rs["bemvindoMsg"].GetType() != typeof(DBNull))
                    {
                        configuracoes = new ConfiguracoesServidor(configuracoes.servidor, new BemVindoGoodByeMsg().setBemvindo((string)rs["bemvindoMsg"]));
                        retorno = true;
                    }
                }
            });
            return Tuple.Create(retorno, configuracoes);
        }

        public async Task<Tuple<bool, ConfiguracoesServidor>> GetByeMsgAsync(ConfiguracoesServidor configuracoes)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetByeMsg(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    if (rs["sairMsg"].GetType() != typeof(DBNull))
                    {
                        configuracoes = new ConfiguracoesServidor(configuracoes.servidor, new ConfiguracoesServidor.BemVindoGoodByeMsg().setSair((string)rs["sairMsg"]));
                        retorno = true;
                    }
                }
            });
            return Tuple.Create(retorno, configuracoes);
        }

        public async Task SetByeMsgAsync(ConfiguracoesServidor configuracoes)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call SetGoodBye(@id, @msg)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
                if (!string.IsNullOrEmpty(configuracoes.bemvindo.sairMsg))
                {
                    cmd.Parameters.AddWithValue("@msg", configuracoes.bemvindo.sairMsg);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@msg", DBNull.Value);
                }

                await cmd.ExecuteNonQueryAsync();
            });
        }

        public async Task<Tuple<bool, ConfiguracoesServidor>> GetErrorMsgAsync(ConfiguracoesServidor configuracoes)
        {
            bool retorno = false;
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call getErrorMessage(@id)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    configuracoes = new ConfiguracoesServidor(configuracoes.servidor, new ErroMsg(Convert.ToBoolean(rs["msgError"])));
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, configuracoes);
        }

        public async Task SetErroMsgAsync(ConfiguracoesServidor configuracoes)
        {
            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call SetErroMsg(@id, @erroMsg)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@id", configuracoes.servidor.Id);
                cmd.Parameters.AddWithValue("@erroMsg", configuracoes.erroMsg.erroMsg);

                await cmd.ExecuteNonQueryAsync();
            });
        }
    }
}
