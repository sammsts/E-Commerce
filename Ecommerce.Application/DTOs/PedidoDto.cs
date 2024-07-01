namespace Ecommerce.Application.DTOs
{
    public class PedidoDto
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

        public int? Ped_Id { get; set; }
        public int[] Prd_Id { get; set; }
        public int Usu_Id { get; set; }
        public int End_Id { get; set; }
        public int Ped_Quantidade { get; set; }
        public FormaPagamentoPedido Ped_FormaPagamento { get; set; }
        public SituacaoPedido Ped_Situacao { get; set; }
        public DateTime? Ped_DataPedido { get; set; }
    }
}
