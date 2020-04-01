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
        protected override void OnConfiguring(DbContextOptionsBuilder config)
        {
            config.UseSqlite($"Data Source={AppDomain.CurrentDomain.BaseDirectory}{DBConst.ConfigName}");
        }
    }
}
