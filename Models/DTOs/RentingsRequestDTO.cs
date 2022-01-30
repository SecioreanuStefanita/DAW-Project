using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class RentingsRequestDTO
    {   
        public  Guid Id { get; set; }
         public Guid UserId { get; set; }
          public Guid ProductsId { get; set; }
           public string? DateStart { get; set; }
        public string? DateFinished { get; set; }

    }
}
