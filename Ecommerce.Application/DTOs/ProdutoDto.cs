using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs
{
    public class ProdutoDto
    {
        public int? Prd_id { get; set; }
        public int Prd_serialId { get; set; }

        [MaxLength(50, ErrorMessage = "Campo de descrição do produto aceita no máximo 50 caracteres.")]
        [StringLength(50)]
        public string Prd_descricao { get; set; }
        public int Prd_quantidadeEstoque { get; set; }
        public DateTime? Prd_dataHoraCadastro { get; set; }
        public string Prd_imgProdutoBase64 { get; set; }
        public byte[] Prd_imgProduto { get; set; }
    }
}
