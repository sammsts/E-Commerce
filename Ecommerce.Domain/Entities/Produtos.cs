using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    [Table("produtos")]
    public partial class Produtos
    {
        [Key]
        [Column("prd_id")]
        public int Prd_id { get; set; }

        [Column("prd_serialid")]
        public int Prd_serialId { get; set; }

        [Required]
        [Column("prd_descricao")]
        [StringLength(50)]
        public string Prd_descricao { get; set; }

        [Required]
        [Column("prd_quantidadeestoque")]
        public int Prd_quantidadeEstoque { get; set; }

        [Required]
        [Column("prd_datahoracadastro")]
        public DateTime Prd_dataHoraCadastro { get; set; }

        [Required]
        [Column("prd_imgproduto")]
        public byte[] Prd_imgProduto { get; set; }

        [Required]
        [Column("prd_valor")]
        public decimal Prd_valor { get; set; }

    }
}
