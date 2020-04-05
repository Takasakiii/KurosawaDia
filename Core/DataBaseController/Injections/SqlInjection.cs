using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DataBaseController.Injections
{
    public static class SqlInjection
    {
        public static void InjectSql(this DatabaseFacade migration, string sql)
        {
            migration.ExecuteSqlRaw(sql);
        }
    }
}
