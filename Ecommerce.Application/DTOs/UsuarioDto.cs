using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Application.DTOs
{
    public class UsuarioDto
    {
        public int? Usu_id { get; set; }

        [Required(ErrorMessage = "O nome é obrigatório.")]
        [StringLength(250)]
        [MaxLength(250, ErrorMessage = "Campo de nome aceita no máximo 250 caracteres.")]
        public string Usu_nome { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(50)]
        [MaxLength(50, ErrorMessage = "Campo de email aceita no máximo 50 caracteres.")]
        public string Usu_email { get; set; }
        public string? Usu_senha { get; set; }
        public bool Usu_IsAdmin { get; set; }
        public string? Usu_ImgPerfilBase64 { get; set; }
        public Byte[]? Usu_ImgPerfil { get; set; }
    }


}
