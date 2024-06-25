using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models
{
    public enum SituacaoCarrinho
    {
        Aberto,
        Fechado
    }

    [Table("carrinho")]
    public partial class Carrinho
    {
        [Key]
        [Column("car_id")]
        public int Car_Id { get; set; }

        [Required]
        [ForeignKey("pedidos")]
        [Column("ped_id")]
        public int Ped_Id { get; set; }

        [Required]
        [ForeignKey("produtos")]
        [Column("prd_id")]
        public int Prd_Id { get; set; }

        [Required]
        [ForeignKey("usuarios")]
        [Column("usu_id")]
        public int Usu_Id { get; set; }

        [Required]
        [ForeignKey("enderecos")]
        [Column("end_id")]
        public int End_Id { get; set; }

        [Required]
        [Column("car_situacao")]
        public SituacaoCarrinho CarSituacao { get; set; }

        public virtual Pedidos Pedido { get; set; }
        public virtual Produtos Produto { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Enderecos Endereco { get; set; }
    }
}
