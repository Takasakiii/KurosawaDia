using MainDatabaseControler.Factory;
using MainDatabaseControler.Modelos;
using MySql.Data.MySqlClient;
using System;

namespace MainDatabaseControler.DAO
{
    public class CargosDAO
    {
        public enum Operacao
        {
            Incompleta = 0,
            Insert = 1,
            Update = 2,
            Delete = 3
        }

        private MySqlConnection conexao = new ConnectionFactory().Conectar();

        public Operacao AdicionarAtualizarCargo(Cargos cargos)
        {
            Operacao retorno = Operacao.Incompleta;
            const string sql = "call AdicionarAtualizarCargoIP(@cargo, @idC, @idS, @IP)";

            MySqlCommand cmd = new MySqlCommand(sql, conexao);
            cmd.Parameters.AddWithValue("@cargo", cargos.Cargo);
            cmd.Parameters.AddWithValue("@idC", cargos.Id);
            cmd.Parameters.AddWithValue("@idS", cargos.Servidor.Id);
            cmd.Parameters.AddWithValue("@IP", cargos.Requesito);

            MySqlDataReader rs = cmd.ExecuteReader();

            if (rs.Read())
            {
                if (rs["tipoOperacao"] != null)
                {
                    retorno = (Operacao)Convert.ToInt32(rs["tipoOperacao"]);
                }
            }

            conexao.Close();
            return retorno;
        }
    }
}
