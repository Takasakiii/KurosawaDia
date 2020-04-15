using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigController.Models
{
    [Table("StatusConfig")]
    public class StatusConfig
    {
        [Column("Cod")]
        [Key]
        public uint Cod { get; set; }

        [Column("StatusJogo")]
        [Required]
        [StringLength(255)]
        public string StatusJogo { get; set; }

        [Column("StatusUrl")]
        [StringLength(255)]
        public string StatusUrl { get; set; }

        [Column("TipoDeStatus", TypeName = "integer")]
        public TipoDeStatus TipoDeStatus { get; set; }
    }
}
