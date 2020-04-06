using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class CanaisConfig : IEntityTypeConfiguration<Canais>
    {
        public void Configure(EntityTypeBuilder<Canais> builder)
        {
            //Table
            builder.ToTable("Canais");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("bigint").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //Nome
            builder.Property(x => x.Nome).HasColumnName("nome").HasColumnType("varchar(255)").HasCharSet("utf8mb4").IsRequired();
            //ID
            builder.Property(x => x.ID).HasColumnName("id").HasColumnType("bigint").IsRequired();
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.Canais).HasForeignKey("codigo_servidor");
        }
    }
}
