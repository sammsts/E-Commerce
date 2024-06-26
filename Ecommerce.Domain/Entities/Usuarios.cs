using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    [Table("usuarios")]
    public partial class Usuarios
    {
        public Usuarios()
        {
            Enderecos = new HashSet<Enderecos>();
        }

        [Key]
        [Column("usu_id")]
        public int Usu_id { get; set; }

        [Required]
        [Column("usu_nome")]
        [StringLength(250)]
        public string Usu_nome { get; set; }

        [Required]
        [Column("usu_email")]
        [StringLength(50)]
        public string Usu_email { get; set; }

        [Required]
        [Column("usu_senha")]
        [StringLength(50)]
        public string Usu_senha { get; set; }

        [Required]
        [Column("usu_status")]
        public bool Usu_status { get; set; }

        [InverseProperty("Usuarios")]
        public virtual ICollection<Enderecos> Enderecos { get; set; }
    }
}
