using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models
{
    [Table("enderecos")]
    public partial class Enderecos
    {
        [Key]
        [Column("end_id")]
        public int End_id { get; set; }

        [Column("usu_id")]
        public int Usu_id { get; set; }

        [Required]
        [Column("end_cep")]
        public int End_cep { get; set; }

        [Required]
        [Column("end_pais")]
        [StringLength(50)]
        public string End_pais { get; set; }

        [Required]
        [Column("end_estado")]
        [StringLength(2)]
        public string End_estado { get; set; }

        [Required]
        [Column("end_bairro")]
        [StringLength(50)]
        public string End_bairro { get; set; }

        [Required]
        [Column("end_rua")]
        [StringLength(50)]
        public string End_rua { get; set; }

        [Required]
        [Column("end_numero")]
        public int End_numero { get; set; }

        [Required]
        [Column("end_complemento")]
        [StringLength(250)]
        public string End_complemento { get; set; }

        [ForeignKey("usu_id")]
        public virtual Usuarios Usuarios { get; set; }
    }
}
