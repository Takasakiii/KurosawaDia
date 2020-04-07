using ConfigController.Constants;
using ConfigController.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigController.EntityConfiguration
{
    public class KurosawaConfigContext : DbContext
    {
        public DbSet<BaseConfig> BaseConfigs { get; set; }
        public DbSet<ApiConfig> ApiConfig { get; set; }
        public DbSet<DBConfig> DBConfig { get; set; }
        public DbSet<StatusConfig> StatusConfig { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder config)
        {
            config.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{DBConst.ConfigName}");
        }
    }
}
