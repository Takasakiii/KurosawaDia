using DataBaseController.Contexts;
using MarinaSQL.Controllers;
using System;
using DataBaseController.Injections;
using System.Threading.Tasks;

namespace MarinaSQL
{
    //Add-Migration KurosawaConfig -Project ConfigController -StartupProject EntityMigrations
    //Add-Migration KurosawaConfig -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext -OutputDir Migrations/DiaImprement
    //Add-Migration KurosawaConfig -Project DataBaseController  -StartupProject EntityMigrations -Context Kurosawa_DiaContext -OutputDir Migrations/KurosawaDatabase
    //Update-Database -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext

    class Program
    {
        static async Task Main(string[] args)
        {
            using (KurosawaMigrationContext context = new KurosawaMigrationContext())
            {
                if (context.Database.EnsureCreated())
                {
                    context.Database.InjectSql(await new SqlsControllers($"{AppDomain.CurrentDomain.BaseDirectory}SQLs").GetSql());
                }
            }    
        }
    }
}
