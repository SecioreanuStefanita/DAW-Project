using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class ProductsRequestDTO
    {    
        public Guid Id { get; set; }
            public string Name { get; set; }
        public string Description { get; set; }

        public string Price { get; set; }
    }
}
