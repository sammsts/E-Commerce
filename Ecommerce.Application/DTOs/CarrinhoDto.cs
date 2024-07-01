namespace Ecommerce.Application.DTOs
{
    public class CarrinhoDto
    {
        public enum SituacaoCarrinho
        {
            Aberto,
            Fechado
        }

        public int? Car_Id { get; set; }
        public int? Ped_Id { get; set; }
        public int Prd_Id { get; set; }
        public int Usu_Id { get; set; }
        public int End_Id { get; set; }
        public SituacaoCarrinho Car_Situacao { get; set; }
    }
}
