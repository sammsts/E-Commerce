using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.API.Models
{
    public enum FormaPagamentoPedido
    {
        Credito,
        AVista
    }

    public enum SituacaoPedido
    {
        EmAndamento,
        Finalizado,
        Cancelado
    }

    [Table("pedidos")]
    public partial class Pedidos
    {
        [Key]
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
        [Column("ped_quantidade")]
        public int Ped_Quantidade { get; set; }

        [Required]
        [Column("ped_formaPagamento")]
        public FormaPagamentoPedido Ped_FormaPagamento { get; set; }

        [Required]
        [Column("ped_situacao")]
        public SituacaoPedido Ped_Situacao { get; set; }

        [Required]
        [Column("ped_dataPedido")]
        public DateTime Ped_DataPedido { get; set; }

        public virtual Produtos Produto { get; set; }
        public virtual Usuarios Usuario { get; set; }
        public virtual Enderecos Endereco { get; set; }
    }
}
