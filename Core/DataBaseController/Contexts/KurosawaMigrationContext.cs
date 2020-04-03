using DataBaseController.Modelos;
using DataBaseController.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.Contexts
{
    public class KurosawaMigrationContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Servidores> Servidores { get; set; }
        public DbSet<ConfiguracoesServidores> ConfiguracoesServidores { get; set; }
        public DbSet<ConfiguracoesServidoresAplicada> ConfiguracoesServidoresAplicadas { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"Server = 127.0.0.1; Port = 3306; Database = Kurosawa_Dia; Uid = takasaki; Pwd = taka123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuariosConfig());
            modelBuilder.ApplyConfiguration(new ServidoresConfig());
            modelBuilder.ApplyConfiguration(new ConfiguracoesServidoresConfig());
            modelBuilder.ApplyConfiguration(new ConfiguracoesServidoresAplicadaConfig());
        }
    }
}
