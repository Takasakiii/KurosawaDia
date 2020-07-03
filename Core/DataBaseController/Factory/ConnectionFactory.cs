using DataBaseController.Contexts;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;

namespace DataBaseController.Factory
{
    internal static class ConnectionFactory
    {
        internal static async Task<MySqlCommand> GetMysqlCommand(this Kurosawa_DiaContext contexto)
        {
            MySqlCommand cmd = (MySqlCommand)contexto.Database.GetDbConnection().CreateCommand();
            await cmd.Connection.OpenAsync();
            return cmd;
        }
    }
}
