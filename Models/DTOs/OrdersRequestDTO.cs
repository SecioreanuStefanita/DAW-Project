using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class OrdersRequestDTO
    { 
         public Guid Id { get; set; }
         public Guid UserId { get; set; }

        public string Price { get; set; }

        public Guid ProductsId { get; set; }
    }
}
