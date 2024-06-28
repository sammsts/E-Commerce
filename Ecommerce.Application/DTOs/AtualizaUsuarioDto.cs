using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Application.DTOs
{
    public class AtualizaUsuarioDto
    {
        [StringLength(250)]
        [MaxLength(250, ErrorMessage = "Campo de nome aceita no máximo 250 caracteres.")]
        public string Usu_nome { get; set; }

        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Campo de email aceita no máximo 50 caracteres.")]
        public string Usu_email { get; set; }

        public string Usu_ImgPerfil { get; set; }
    }
}
