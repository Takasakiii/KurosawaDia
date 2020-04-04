using DataBaseController.Modelos;
using DataBaseController.ModelsConfiguration;
using Microsoft.EntityFrameworkCore;

namespace DataBaseController.Contexts
{
    public class KurosawaMigrationContext : DbContext
    {
        public DbSet<Usuarios> Usuarios { get; set; }
        public DbSet<Servidores> Servidores { get; set; }
        public DbSet<Servidores_Usuarios> Servidores_Usuarios { get; set; }
        public DbSet<ConfiguracoesServidores> ConfiguracoesServidores { get; set; }
        public DbSet<ConfiguracoesServidoresAplicada> ConfiguracoesServidoresAplicadas { get; set; }
        public DbSet<AdmsBot> AdmsBots { get; set; }
        public DbSet<Canais> Canais { get; set; }
        public DbSet<Cargos> Cargos { get; set; }
        public DbSet<CustomReactions> CustomReactions { get; set; }
        public DbSet<Fuck> Fuck { get; set; }
        public DbSet<Insultos> Insultos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql($"Server = 127.0.0.1; Port = 3306; Database = Kurosawa_Dia; Uid = imprementacao; Pwd = Imprementacao@123;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuariosConfig());
            modelBuilder.ApplyConfiguration(new ServidoresConfig());
            modelBuilder.ApplyConfiguration(new Servidores_UsuariosConfig());
            modelBuilder.ApplyConfiguration(new ConfiguracoesServidoresConfig());
            modelBuilder.ApplyConfiguration(new ConfiguracoesServidoresAplicadaConfig());
            modelBuilder.ApplyConfiguration(new AdmsBotConfig());
            modelBuilder.ApplyConfiguration(new CanaisConfig());
            modelBuilder.ApplyConfiguration(new CargosConfig());
            modelBuilder.ApplyConfiguration(new CustomReactionConfig());
            modelBuilder.ApplyConfiguration(new FuckConfig());
            modelBuilder.ApplyConfiguration(new InsultosConfig());
        }
    }
}
