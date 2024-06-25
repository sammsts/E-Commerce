using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models
{
    [Table("produtos")]
    public partial class Produtos
    {
        [Key]
        [Column("prd_id")]
        public int Prd_id { get; set; }

        [Required]
        [Column("prd_descricao")]
        [StringLength(50)]
        public string Prd_descricao { get; set; }

        [Required]
        [Column("prd_quantidadeEstoque")]
        public int Prd_quantidadeEstoque { get; set; }

        [Required]
        [Column("prd_dataHoraCadastro")]
        public DateTime Prd_dataHoraCadastro { get; set; }
    }
}
