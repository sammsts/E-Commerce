using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models
{
    [Table("log")]
    public partial class Log
    {
        [Key]
        [Column("log_id")]
        public int Prd_id { get; set; }

        [Required]
        [Column("log_idUsuario")]
        public int Log_idUsuario { get; set; }

        [Required]
        [Column("log_nomeUsuario")]
        [StringLength(50)]
        public string Log_nomeUsuario { get; set; }

        [Required]
        [Column("log_emailUsuario")]
        [StringLength(50)]
        public string Log_emailUsuario { get; set; }

        [Required]
        [Column("log_descricao")]
        [StringLength(250)]
        public string Log_descricao { get; set; }
    }
}
