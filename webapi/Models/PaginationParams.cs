using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models
{
    public class PaginationParams
    {
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; }

        [Range(1, 50, ErrorMessage = "O máximo de itens por página é 50.")]
        public int pageSize { get; set; }
    }
}
