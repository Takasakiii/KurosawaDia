using DataBaseController.Contexts;
using DataBaseController.Procedures;
using System;

namespace EntityMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            using (KurosawaMigrationContext context = new KurosawaMigrationContext())
            {
                context.Database.EnsureCreated();
                context.Database.PitasCornoCreateProcedure();
            }

            Console.ReadKey();
            //Add-Migration KurosawaConfig -Project ConfigController -StartupProject EntityMigrations
            //Add-Migration KurosawaConfig -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext -OutputDir Migrations/DiaImprement
            //Add-Migration KurosawaConfig -Project DataBaseController  -StartupProject EntityMigrations -Context Kurosawa_DiaContext -OutputDir Migrations/KurosawaDatabase
            //Update-Database -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext
        }
    }
}
