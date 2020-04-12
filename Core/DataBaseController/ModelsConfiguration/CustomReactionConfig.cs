using DataBaseController.Modelos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBaseController.ModelsConfiguration
{
    internal sealed class CustomReactionConfig : IEntityTypeConfiguration<CustomReactions>
    {
        public void Configure(EntityTypeBuilder<CustomReactions> builder)
        {
            //Table
            builder.ToTable("CustomReactions");
            //Cod
            builder.HasKey(x => x.Cod);
            builder.Property(x => x.Cod).HasColumnName("cod_cr").HasColumnType("bigint").HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
            //Trigger
            builder.Property(x => x.Trigger).HasColumnName("trigger_cr").HasColumnType("text").HasCharSet("utf8mb4").IsRequired();
            //Resposta
            builder.Property(x => x.Resposta).HasColumnName("resposta_cr").HasColumnType("text").HasCharSet("utf8mb4").IsRequired();
            //Modo
            builder.Property(x => x.Modo).HasColumnName("modo_cr").HasColumnType("bool").IsRequired();
            //Servidor
            builder.HasOne(x => x.Servidor).WithMany(x => x.CustomReactions).HasForeignKey("servidor_cr").IsRequired();
        }
    }
}
