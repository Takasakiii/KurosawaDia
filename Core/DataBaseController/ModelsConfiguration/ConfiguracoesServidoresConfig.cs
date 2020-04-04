using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class ConfiguracoesServidoresConfig : IEntityTypeConfiguration<ConfiguracoesServidores>
    {
        public void Configure(EntityTypeBuilder<ConfiguracoesServidores> builder)
        {
            //Table
            builder.ToTable("ConfiguracoesServidores");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("int");
            //Key
            builder.Property(x => x.Key).HasColumnName("key").HasColumnType("varchar(255)").IsRequired();
            builder.HasIndex(x => x.Key).IsUnique();
        }
    }
}
