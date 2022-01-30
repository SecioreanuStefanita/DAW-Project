using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class OrdersCreateDTO
    { 
         public Guid UserId { get; set; }

        public Guid ProductsId { get; set; }
    }
}
