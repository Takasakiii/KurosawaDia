using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using System.Threading.Tasks;

namespace Bot.DB.DAO
{
    public class TokensDAO
    {
        SQLiteConnection connection = new ConnectionFactory().conectar();
    }
}
