using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace MainDatabaseControler.DAO
{
    public class PontosInterativosDAO
    {
        public async Task<Tuple<bool, PontosInterativos, PI, Cargos>> AdicionarPontoAsync(PontosInterativos pontosInterativos, PI piSaida, Cargos cargoSaida)
        {
            bool retorno = false;
            cargoSaida = null;
            piSaida = new PI();

            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call AddPI(@servidor, @usuario)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);
                cmd.Parameters.AddWithValue("@servidor", pontosInterativos.Servidores_Usuarios.Servidor.Id);
                cmd.Parameters.AddWithValue("@usuario", pontosInterativos.Servidores_Usuarios.Usuario.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    if (Convert.ToBoolean(rs["Upou"]))
                    {
                        pontosInterativos.AddPIInfo(0, Convert.ToUInt64(rs["LevelAtual"]), 0);
                        if (rs["CargoID"] != DBNull.Value)
                        {
                            cargoSaida = new Cargos(tipos_Cargos: Cargos.Tipos_Cargos.XpRole, id: Convert.ToUInt64(rs["CargoID"]), cargo: "", requesito: Convert.ToInt64(pontosInterativos.PI), pontosInterativos.Servidores_Usuarios.Servidor);
                        }
                        if (rs["MsgPIUp"] != DBNull.Value)
                        {
                            piSaida = new PI(true, MsgPIUp: (string)rs["MsgPIUp"]);
                            retorno = true;
                        }
                    }
                }
            });
            return Tuple.Create(retorno, pontosInterativos, piSaida, cargoSaida);
        }
        public async Task<Tuple<bool, ulong, PontosInterativos>> GetPiInfoAsync(PontosInterativos pi)
        {
            bool retorno = false;
            ulong total = 0;

            await ConnectionFactory.ConectarAsync(async (conexao) =>
            {
                const string sql = "call GetPiInfo(@idUsuario, @idServidor)";
                MySqlCommand cmd = new MySqlCommand(sql, conexao);

                cmd.Parameters.AddWithValue("@idUsuario", pi.Servidores_Usuarios.Usuario.Id);
                cmd.Parameters.AddWithValue("@idServidor", pi.Servidores_Usuarios.Servidor.Id);

                DbDataReader rs = await cmd.ExecuteReaderAsync();
                if (await rs.ReadAsync())
                {
                    pi = new PontosInterativos(pi.Servidores_Usuarios).AddPIInfo(Convert.ToUInt64(rs["cod"]), Convert.ToUInt64(rs["PI"]), Convert.ToUInt64(rs["fragmentosPI"]));
                    total = Convert.ToUInt64(rs["total"]);
                    retorno = true;
                }
            });
            return Tuple.Create(retorno, total, pi);
        }
    }
}
