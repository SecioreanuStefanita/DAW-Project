using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class UserDetailsRequestDTO
    {   public Guid Id { get; set; }
            public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string BusinessDetails { get; set; }
        public Guid UserId { get; set; }

    }
}
