using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class InsultosConfig : IEntityTypeConfiguration<Insultos>
    {
        public void Configure(EntityTypeBuilder<Insultos> builder)
        {
            //Table
            builder.ToTable("Insultos");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod").HasColumnType("bigint");
            //Usuario
            builder.HasOne(x => x.Usuario).WithMany(x => x.Insultos).HasForeignKey("usuario");
            //Insulto
            builder.Property(x => x.Insulto).HasColumnName("insulto").HasColumnType("text").HasCharSet("utf8").IsRequired();
        }
    }
}
