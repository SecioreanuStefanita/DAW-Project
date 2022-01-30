using System;
using Proiect.Models.Base;
using System.Text.Json.Serialization;
namespace Proiect.Models
{
    public class UserDetails: BaseEntity
    {
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public string BusinessDetails { get; set; }
        public Guid UserId { get; set; }

         public User User { get; set; }

    }
}
