using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigController.Models
{
    [Table("BaseConfig")]
    public sealed class BaseConfig
    {
        [Column("Cod")]
        [Key]
        public uint Cod { get; set; }
        [Column("TokenBot")]
        [Required]
        public string Token { get; set; }
        [Column("Prefix")]
        [Required]
        [StringLength(16)]
        public string Prefixo { get; set; } = "~";
        [Column("IDDono")]
        [Required]
        public ulong IdDono { get; set; }
    }
}
