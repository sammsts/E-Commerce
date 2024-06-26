﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Domain.Entities
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
        public int[] Prd_Id { get; set; }

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
        [Column("ped_formapagamento")]
        public FormaPagamentoPedido Ped_FormaPagamento { get; set; }

        [Required]
        [Column("ped_situacao")]
        public SituacaoPedido Ped_Situacao { get; set; }

        [Required]
        [Column("ped_datapedido")]
        public DateTime Ped_DataPedido { get; set; }
    }
}
