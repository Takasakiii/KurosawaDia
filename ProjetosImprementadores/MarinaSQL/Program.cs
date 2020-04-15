using DataBaseController.Contexts;
using MarinaSQL.Controllers;
using System;
using DataBaseController.Injections;
using System.Threading.Tasks;

namespace MarinaSQL
{
    //Add-Migration KurosawaConfig -Project ConfigController -StartupProject MarinaSQL
    //Add-Migration KurosawaConfig -Project DataBaseController  -StartupProject MarinaSQL -Context Kurosawa_DiaContext -OutputDir Migrations/KurosawaDatabase
    //Update-Database -Project DataBaseController -StartupProject MarinaSQL -Context KurosawaMigrationContext

    class Program
    {
        static async Task Main(string[] args)
        {
            using (Kurosawa_DiaContext context = new Kurosawa_DiaContext())
            {
                if (context.Database.EnsureCreated())
                {
                    context.Database.InjectSql("SET GLOBAL log_bin_trust_function_creators = 1;\n\n" + await new SqlsControllers($"{AppDomain.CurrentDomain.BaseDirectory}SQLs").GetSql());
                }
            }    
        }
    }
}
