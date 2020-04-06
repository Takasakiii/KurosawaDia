using DataBaseController.Abstractions;
using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class CargosConfig : IEntityTypeConfiguration<Cargos>
    {

        public void Configure(EntityTypeBuilder<Cargos> builder)
        {
            //Table
            builder.ToTable("Cargos");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("bigint").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //TipoCargo
            builder.Property(x => x.TipoCargo).HasColumnName("tipoCargos").HasColumnType("tinyint").HasConversion<byte>().IsRequired().HasDefaultValue(TipoCargos.Default);
            //Nome
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(255)").HasCharSet("utf8mb4").IsRequired();
            //ID
            builder.Property(x => x.ID).HasColumnName("id").HasColumnType("bigint").IsRequired();
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.Cargos).HasForeignKey("codigo_servidor");
        }
    }
}
