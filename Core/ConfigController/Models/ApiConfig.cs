using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ConfigController.Models
{
    [Table("ApiConfig")]
    public class ApiConfig
    {
        [Column("Cod")]
        [Key]
        public uint Cod { get; set; }
        [Column("Nome")]
        [Required]
        [StringLength(100)]
        public string Nome { get; set; }
        [Column("Key")]
        [Required]
        public string Key { get; set; }
    }
}
