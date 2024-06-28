using Ecommerce.Domain.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
{
    [Table("usuarios")]
    public partial class Usuarios
    {
        public Usuarios()
        {
            //Enderecos = new HashSet<Enderecos>();
        }

        public Usuarios(int _usu_id, string _usu_nome, string _usu_email)
        {
            DomainExceptionValidation.When(_usu_id < 0, "O id não pode ser negativo.");
            Usu_id = _usu_id;
            ValidateDomain(_usu_nome, _usu_email);
        }

        public Usuarios(string _usu_nome, string _usu_email)
        {
            ValidateDomain(_usu_nome, _usu_email);
        }

        [Key]
        [Column("usu_id")]
        public int Usu_id { get; private set; }

        [Required]
        [Column("usu_nome")]
        [StringLength(250)]
        public string Usu_nome { get; private set; }

        [Required]
        [Column("usu_email")]
        [StringLength(250)]
        public string Usu_email { get; private set; }

        [Required]
        [Column("usu_senhahash")]
        [StringLength(50)]
        public byte[] Usu_senhaHash { get; private set; }

        [Required]
        [Column("usu_senhasalt")]
        [StringLength(50)]
        public byte[] Usu_senhaSalt { get; private set; }

        [Required]
        [Column("usu_status")]
        public bool Usu_status { get; set; }

        [Required]
        [Column("usu_isadmin")]
        public bool Usu_IsAdmin { get; set; }

        [Column("usu_imagemperfil")]
        public byte[]? Usu_ImgPerfil { get; set; }

        //[InverseProperty("Usuarios")]
        //public virtual ICollection<Enderecos> Enderecos { get; set; }

        public void SetAdmin(bool isAdmin)
        {
            Usu_IsAdmin = isAdmin;
        }

        public void AlterarSenha(byte[] _usu_senhaHash, byte[] _usu_senhaSalt)
        {
            Usu_senhaHash = _usu_senhaHash;
            Usu_senhaSalt = _usu_senhaSalt;
        }

        public void ValidateDomain(string _usu_nome, string _usu_email)
        {
            DomainExceptionValidation.When(_usu_nome == null, "O nome é obrigatório.");
            DomainExceptionValidation.When(_usu_email == null, "O e-mail é obrigatório.");
            DomainExceptionValidation.When(_usu_nome.Length > 250, "O nome não pode ultrapassar de 250 caracteres.");
            DomainExceptionValidation.When(_usu_email.Length > 250, "O e-mail não pode ultrapassar de 250 caracteres.");
            Usu_nome = _usu_nome;
            Usu_email = _usu_email;
            Usu_status = true; 
            Usu_IsAdmin = false;
        }
    }
}
