using DataBaseController.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
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
