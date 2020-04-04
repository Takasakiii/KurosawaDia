using System;

namespace EntityMigrations
{
    class Program
    {
        static void Main(string[] args)
        {
            //Add-Migration KurosawaConfig -Project ConfigController -StartupProject EntityMigrations
            //Add-Migration KurosawaConfig -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext -OutputDir Migrations/DiaImprement
            //Add-Migration KurosawaConfig -Project DataBaseController  -StartupProject EntityMigrations -Context Kurosawa_DiaContext -OutputDir Migrations/KurosawaDatabase
            //Update-Database -Project DataBaseController -StartupProject EntityMigrations -Context KurosawaMigrationContext
        }
    }
}
