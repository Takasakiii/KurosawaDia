using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataBaseController.ModelsConfiguration
{
    class ConfiguracoesServidoresAplicadaConfig : IEntityTypeConfiguration<ConfiguracoesServidoresAplicada>
    {
        public void Configure(EntityTypeBuilder<ConfiguracoesServidoresAplicada> builder)
        {
            //Table
            builder.ToTable("ConfiguracoesServidoresAplicada");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("bigint");
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.Configuracoes).HasForeignKey("servidor");
            //ConfiguracoesServidores
            builder.HasOne(x => x.Configuracao).WithMany(x => x.Configuracoes).HasForeignKey("configuracoes");
            //Value
            builder.Property(x => x.Value).HasColumnName("valor").IsRequired();
        }
    }
}
