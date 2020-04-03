using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigController.Models
{
    [Table("DBConfig")]
    public class DBConfig
    {
        [Column("Cod")]
        [Key]
        public ushort Cod { get; set; }
        [Column("IP")]
        [Required]
        public string IP { get; set; }
        [Column("Porta")]
        [Required]
        public uint Porta { get; set; }
        [Column("User")]
        [Required]
        public string User { get; set; }
        [Column("Senha")]
        [Required]
        public string Senha { get; set; }
        [Column("Database")]
        [Required]
        public string Database { get; set; }
    }
}
