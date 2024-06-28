using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Application.DTOs
{
    public class EnderecoDto
    {
        public int End_id { get; set; }

        public int Usu_id { get; set; }

        public int End_cep { get; set; }

        [MaxLength(50, ErrorMessage = "Campo de país aceita no máximo 50 caracteres.")]
        [StringLength(50)]
        public string End_pais { get; set; }

        [MaxLength(2, ErrorMessage = "Campo de estado aceita no máximo 2 caracteres.")]
        [StringLength(2)]
        public string End_estado { get; set; }

        [MaxLength(50, ErrorMessage = "Campo de cidade aceita no máximo 50 caracteres.")]
        [StringLength(50)]
        public string End_cidade { get; set; }

        [MaxLength(50, ErrorMessage = "Campo de bairro aceita no máximo 50 caracteres.")]
        [StringLength(50)]
        public string End_bairro { get; set; }

        [MaxLength(50, ErrorMessage = "Campo de rua aceita no máximo 50 caracteres.")]
        [StringLength(50)]
        public string End_rua { get; set; }

        public int End_numero { get; set; }

        [StringLength(250)]
        public string End_complemento { get; set; }
    }
}
