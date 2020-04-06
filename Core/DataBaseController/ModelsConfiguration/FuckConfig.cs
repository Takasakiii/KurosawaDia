using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class FuckConfig : IEntityTypeConfiguration<Fuck>
    {
        public void Configure(EntityTypeBuilder<Fuck> builder)
        {
            //Table
            builder.ToTable("Fuck");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("bigint").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //Usuario
            builder.HasOne(x => x.Usuario).WithMany(x => x.Fuck).HasForeignKey("codigo_usuario");
            //URL
            builder.Property(x => x.Url).HasColumnName("urlImage").HasColumnType("varchar(255)").IsRequired();
            //Explicit
            builder.Property(x => x.Explicit).HasColumnName("explicitImage").HasColumnType("bool").IsRequired().HasDefaultValue(false);
        }
    }
}
