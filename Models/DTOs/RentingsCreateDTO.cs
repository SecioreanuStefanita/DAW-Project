using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class RentingsCreateDTO
    { 
         public Guid UserId { get; set; }

        public string? DateStart { get; set; }
        public string? DateFinished { get; set; }

        public Guid ProductsId { get; set; }
    }
}
