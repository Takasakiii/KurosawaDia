using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;
using static MainDatabaseControler.Modelos.ConfiguracoesServidor;

namespace MainDatabaseControler.DAO
{
    public class PontosInterativosDAO
    {
        private MySqlConnection conexao = null;

        public PontosInterativosDAO()
        {
            conexao = new ConnectionFactory().Conectar();
        }

        public bool AdicionarPonto(ref PontosInterativos pontosInterativos, out PI piSaida, out Cargos cargoSaida)
        {
            bool retorno = false;
            const string sql = "call AddPI(@servidor, @usuario)";
            cargoSaida = null;
            piSaida = new PI();
            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@servidor", pontosInterativos.Servidores_Usuarios.Servidor.Id);
            cmd.Parameters.AddWithValue("@usuario", pontosInterativos.Servidores_Usuarios.Usuario.Id);

            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
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
            conexao.Close();
            return retorno;
        }
        public Tuple<bool, ulong> GetPiInfo(ref PontosInterativos pi)
        {
            const string sql = "call GetPiInfo(@idUsuario, @idServidor)";
            MySqlCommand cmd = new MySqlCommand(sql, conexao);

            cmd.Parameters.AddWithValue("@idUsuario", pi.Servidores_Usuarios.Usuario.Id);
            cmd.Parameters.AddWithValue("@idServidor", pi.Servidores_Usuarios.Servidor.Id);

            bool retorno = false;
            ulong total = 0;

            MySqlDataReader rs = cmd.ExecuteReader();
            if (rs.Read())
            {
                pi = new PontosInterativos(pi.Servidores_Usuarios).AddPIInfo(Convert.ToUInt64(rs["cod"]), Convert.ToUInt64(rs["PI"]), Convert.ToUInt64(rs["fragmentosPI"]));
                total = Convert.ToUInt64(rs["total"]);
                retorno = true;
            }
            rs.Close();
            conexao.Close();
            return Tuple.Create(retorno, total);
        }
    }
}
