using DataBaseController.Modelos;
using DataBaseController.ModelsConfiguration;
using DataBaseController.Singletons;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Contexts
{
    public class Kurosawa_DiaContext : DbContext
    {
        public DbSet<Usuarios> Usuario { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"Server = {DBDataSingleton.ConfigDB.IP}; Port = {DBDataSingleton.ConfigDB.Porta}; Database = {DBDataSingleton.ConfigDB.Database}; Uid = {DBDataSingleton.ConfigDB.User}; Pwd = {DBDataSingleton.ConfigDB.Senha};");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuariosConfig());
        }
    }
}
