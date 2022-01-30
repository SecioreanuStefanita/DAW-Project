using System.ComponentModel.DataAnnotations;

namespace Proiect.Models.DTOs
{
    public class UserCRUDRequestDTO
    {
        public Guid Id { get; set; }
        public string Username { get; set; }

        public string Email { get; set; }
          public Role Role { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
