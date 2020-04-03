﻿using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    public class ServidoresConfig : IEntityTypeConfiguration<Servidores>
    {
        public void Configure(EntityTypeBuilder<Servidores> builder)
        {
            //Table
            builder.ToTable("Servidores");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("codigo_servidor").HasColumnType("bigint");
            //ID
            builder.Property(x => x.ID).HasColumnName("id_servidor").HasColumnType("bigint").IsRequired();
            builder.HasIndex(x => x.ID).IsUnique();
            //Nome
            builder.Property(x => x.Nome).HasColumnName("nome_servidor").HasColumnType("varchar(255)").IsRequired();
            //Especial
            builder.Property(x => x.Espercial).HasColumnName("especial_servidor").HasColumnType("bool").IsRequired().HasDefaultValue(false);
            //Prefix
            builder.Property(x => x.Prefix).HasColumnName("prefix_servidor").HasColumnType("varchar(25)").HasDefaultValue(null);
        }
    }
}
